using System;

namespace Cession.Commands
{
    public class ZeroArgumentCommand:Command
    {
        private Action executeFunc;
        private Action undoFunc;

        public ZeroArgumentCommand (Action executeFunc, Action undoFunc)
        {
            if (null == executeFunc)
                throw new ArgumentNullException ();

            if (null == undoFunc)
                throw new ArgumentNullException ();

            this.executeFunc = executeFunc;
            this.undoFunc = undoFunc;
        }

        public override void Execute ()
        {
            executeFunc.Invoke ();
        }

        public override void Undo ()
        {
            undoFunc.Invoke ();
        }
    }
}

