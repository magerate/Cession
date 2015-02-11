using System;

namespace Cession.Commands
{
    public class OneArgumentCommand<T>:Command
    {
        private T arg;
        private Action<T> executeFunc;
        private Action<T> undoFunc;

        public OneArgumentCommand (T arg,
                             Action<T> executeFunc, 
                             Action<T> undoFunc)
        {
            if (null == executeFunc)
                throw new ArgumentNullException ();

            if (null == undoFunc)
                throw new ArgumentNullException ();

            this.arg = arg;
            this.executeFunc = executeFunc;
            this.undoFunc = undoFunc;
        }

        public OneArgumentCommand (T arg,
                             Action executeFunc, 
                             Action<T> undoFunc) :
            this (arg, a => executeFunc (), undoFunc)
        {
        }

        public OneArgumentCommand (T arg,
                             Action<T> executeFunc, 
                             Action undoFunc) :
            this (arg, executeFunc, a => undoFunc ())
        {
        }


        public override void Undo ()
        {
            undoFunc.Invoke (arg);
        }

        public override void Execute ()
        {
            executeFunc.Invoke (arg);
        }
    }
}

