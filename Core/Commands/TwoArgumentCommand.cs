using System;

namespace Cession.Commands
{
    public class TwoArgumentCommand<T1,T2>:Command
    {
        private T1 arg1;
        private T2 arg2;

        private Action<T1,T2> executeFunc;
        private Action<T1,T2> undoFunc;

        public TwoArgumentCommand (T1 arg1, 
                             T2 arg2, 
                             Action<T1,T2> executeFunc, 
                             Action<T1,T2> undoFunc)
        {
            if (null == executeFunc)
                throw new ArgumentNullException ();

            if (null == undoFunc)
                throw new ArgumentNullException ();

            this.arg1 = arg1;
            this.arg2 = arg2;

            this.executeFunc = executeFunc;
            this.undoFunc = undoFunc;
        }

        public TwoArgumentCommand (T1 arg1, 
                             T2 arg2, 
                             Action<T1> executeFunc, 
                             Action<T1,T2> undoFunc) :
            this (arg1, arg2, (a1, a2) => executeFunc (a1), undoFunc)
        {
        }

        public TwoArgumentCommand (T1 arg1, 
                             T2 arg2, 
                             Action<T1,T2> executeFunc, 
                             Action<T1> undoFunc) :
            this (arg1, arg2, executeFunc, (a1, a2) => undoFunc (a1))
        {
        }

        public override void Execute ()
        {
            executeFunc.Invoke (arg1, arg2);
        }

        public override void Undo ()
        {
            undoFunc.Invoke (arg1, arg2);
        }
    }
}

