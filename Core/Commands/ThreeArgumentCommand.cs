namespace Cession.Commands
{
	using System;

	public class ThreeArgumentCommand<T1,T2,T3>:Command
	{
		private T1 arg1;
		private T2 arg2;
		private T3 arg3;
		private Action<T1,T2,T3> executeFunc;
		private Action<T1,T2,T3> undoFunc;

		public ThreeArgumentCommand (T1 arg1, 
		                           T2 arg2, 
		                           T3 arg3,
		                           Action<T1,T2,T3> executeFunc, 
		                           Action<T1,T2,T3> undoFunc)
		{
			if(null == executeFunc)
				throw new ArgumentNullException();

			if(null == undoFunc)
				throw new ArgumentNullException();

			this.arg1 = arg1;
			this.arg2 = arg2;
			this.arg3 = arg3;
			
			this.executeFunc = executeFunc;
			this.undoFunc = undoFunc;
		}
		
		public override void Execute ()
		{
			executeFunc.Invoke (arg1, arg2, arg3);
		}
		
		public override void Undo ()
		{
			undoFunc.Invoke (arg1, arg2, arg3);
		}
	}
}

