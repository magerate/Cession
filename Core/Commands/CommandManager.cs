using System;
using System.Collections.Generic;

namespace Cession.Commands
{
    public partial class CommandManager
    {
        private Stack<Command> undoStack;
        private Stack<Command> redoStack;

        private Queue<Command> commandQueue;

        public event EventHandler<EventArgs> Committed;

        public event EventHandler<EventArgs> CanUndoChanged;
        public event EventHandler<EventArgs> CanRedoChanged;

        public CommandManager ()
        {
            undoStack = new Stack<Command> ();
            redoStack = new Stack<Command> ();

            commandQueue = new Queue<Command> ();
        }

        public void Execute (Command command)
        {
            command.Execute ();
            Push (command);
        }


        private void OnCommit ()
        {
            if (null != Committed)
                Committed (this, EventArgs.Empty);
        }

        private void OnUndoChange ()
        {
            if (null != CanUndoChanged)
                CanUndoChanged (this, EventArgs.Empty);
        }

        private void OnRedoChange ()
        {
            if (null != CanRedoChanged)
                CanRedoChanged (this, EventArgs.Empty);
        }


        public void Push ()
        {
            if (commandQueue.Count == 0)
                return;

            if (commandQueue.Count == 1)
            {
                Push (commandQueue.Dequeue ());
                return;
            }

            var mc = new MacroCommand (commandQueue);
            commandQueue.Clear ();
            Push (mc);
        }

        public void Push (Command command)
        {
            ClearRedoStack ();

            if (commandQueue.Count > 0)
            {
                var mc = new MacroCommand (commandQueue);
                mc.AddCommand (command);
                commandQueue.Clear ();
                undoStack.Push (mc);
            } else
                undoStack.Push (command);

            CheckChanged (true, undoStack, OnUndoChange);

            OnCommit ();
        }

        private void ClearRedoStack ()
        {
            if (redoStack.Count > 0)
            {
                redoStack.Clear ();
                OnRedoChange ();
            }
        }

        public void ExecuteQueue (Command command)
        {
            ClearRedoStack ();
            command.Execute ();
            commandQueue.Enqueue (command);
        }

        public void Queue (Command command)
        {
            commandQueue.Enqueue (command);
        }

        public void Undo ()
        {
            if (commandQueue.Count > 0)
                throw new InvalidOperationException ("command queue should be empty when undo, you may misuse the ExecuteQueue method");

            if (undoStack.Count <= 0)
                throw new InvalidOperationException ("undo stake is empty");

            var command = undoStack.Pop ();
            CheckChanged (false, undoStack, OnUndoChange);

            redoStack.Push (command);
            CheckChanged (true, redoStack, OnRedoChange);

            Commit (command.Undo);
        }

        public void Redo ()
        {
            if (redoStack.Count <= 0)
                throw new InvalidOperationException ("redo stake is empty");

            var command = redoStack.Pop ();
            CheckChanged (false, redoStack, OnRedoChange);

            undoStack.Push (command);
            CheckChanged (true, undoStack, OnUndoChange);

            Commit (command.Execute);
        }

        private void Commit (Action action)
        {
            action ();
            OnCommit ();
        }

        private void CheckChanged (bool isPush, Stack<Command> stack, Action action)
        {
            if (isPush)
            {
                if (stack.Count == 1)
                    action ();
            } else
            {
                if (stack.Count == 0)
                    action ();
            }
        }

        public void Clear ()
        {
            undoStack.Clear ();
            redoStack.Clear ();
            commandQueue.Clear ();
        }

        public bool CanUndo
        {
            get{ return undoStack.Count > 0; }
        }

        public bool CanRedo
        {
            get{ return redoStack.Count > 0; }
        }
    }
}

