using System;

namespace Cession.Commands
{
    public class FourArgumentCommand<T1,T2,T3,T4>:Command
    {
        private T1 arg1;
        private T2 arg2;
        private T3 arg3;
        private T4 arg4;

        private Action<T1,T2,T3,T4> executeFunc;
        private Action<T1,T2,T3,T4> undoFunc;

        public FourArgumentCommand (T1 arg1, 
                              T2 arg2, 
                              T3 arg3,
                              T4 arg4,
                              Action<T1,T2,T3,T4> executeFunc, 
                              Action<T1,T2,T3,T4> undoFunc)
        {
            if (null == executeFunc)
                throw new ArgumentNullException ();

            if (null == undoFunc)
                throw new ArgumentNullException ();

            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
            this.arg4 = arg4;

            this.executeFunc = executeFunc;
            this.undoFunc = undoFunc;
        }

        public override void Execute ()
        {
            executeFunc.Invoke (arg1, arg2, arg3, arg4);
        }

        public override void Undo ()
        {
            undoFunc.Invoke (arg1, arg2, arg3, arg4);
        }
    }
}

