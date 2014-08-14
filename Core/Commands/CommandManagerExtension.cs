namespace Cession.Commands
{
	using System;
	using System.Collections.Generic;


	public static class CommandManagerExtension
	{
		private static void Execute(CommandManager commandManager,Command command,bool isPush)
		{
			if (isPush)
				commandManager.Execute (command);
			else
				commandManager.ExecuteQueue (command);
		}

		public static void Execute(this CommandManager commandManager,
									Action action,
									bool isPush = true)
		{
			var command = new ZeroArgumentCommand (action, action);
			Execute (commandManager, command, isPush);
		}

		public static void Execute(this CommandManager commandManager,
									Action executeAction,
									Action undoAction,
									bool isPush = true)
		{
			var command = new ZeroArgumentCommand (executeAction, undoAction);
			Execute (commandManager, command, isPush);
		}

		public static void Execute<T>(this CommandManager commandManager,
			T value,
			Action<T> executeAction,
			Action<T> undoAction,
			bool isPush = true)
		{
			var command = new OneArgumentCommand<T> (value, executeAction, undoAction);
			Execute (commandManager, command, isPush);
		}


		public static void ExecuteClear<T>(this CommandManager commandManager,
											ICollection<T> collection,
											bool isPush = true)
		{
			var command = Command.CreateClear (collection);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteDictionaryAdd<TKey,TValue>(this CommandManager commandManager,
											IDictionary<TKey,TValue> dictionary,
											TKey key,
											TValue value,
											bool isPush = true)
		{
			var command = Command.CreateDictionaryAdd (dictionary, key, value);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteDictionaryRemove<TKey,TValue>(this CommandManager commandManager,
															IDictionary<TKey,TValue> dictionary,
															TKey key,
															bool isPush = true)
		{
			var command = Command.CreateDictionaryRemove (dictionary, key);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteDictionaryReplace<TKey,TValue>(this CommandManager commandManager,
											IDictionary<TKey,TValue> dictionary,
											TKey key,
											TValue value,
											bool isPush = true)
		{
			var command = Command.CreateDictionaryReplace (dictionary, key,value);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteListInsert<T>(this CommandManager commandManager,
													IList<T> list,
													int index,
													T value,
													bool isPush = true)
		{
			var command = Command.CreateListInsert (list, index, value);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteListAdd<T>(this CommandManager commandManager,
								IList<T> list,
								T value,
								bool isPush = true)
		{
			var command = Command.CreateListAdd (list, value);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteListRemove<T>(this CommandManager commandManager,
			IList<T> list,
			int index,
			bool isPush = true)
		{
			var command = Command.CreateListRemove (list, index);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteListRemove<T>(this CommandManager commandManager,
			IList<T> list,
			T item,
			bool isPush = true)
		{
			var command = Command.CreateListRemove (list, item);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteListReplace<T>(this CommandManager commandManager,
			IList<T> list,
			int index,
			T value,
			bool isPush = true)
		{
			var command = Command.CreateListReplace (list,index,value);
			Execute (commandManager, command, isPush);
		}

		public static void Execute<T>(this CommandManager commandManager,
			T newValue,
			T oldValue,
			Action<T> action,
			bool isPush = true)
		{
			var command = Command.Create (newValue, oldValue, action);
			Execute (commandManager, command, isPush);
		}

		public static void ExecuteSetProperty<T1,T2>(this CommandManager commandManager,
			T1 target,
			T2 value,
			string propertyName,
			bool isPush = true)
		{
			var command = Command.CreateSetProperty (target, value, propertyName);
			Execute (commandManager, command, isPush);
		}

	}
}

