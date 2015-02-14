using System;
using System.Collections.Generic;

namespace Cession.Commands
{
    public partial class CommandManager
    {
        private void Execute (Command command, bool isPush)
        {
            if (isPush)
                Execute (command);
            else
                ExecuteQueue (command);
        }

        public void Execute (Action action, bool isPush = true)
        {
            var command = new ZeroArgumentCommand (action, action);
            Execute (command, isPush);
        }

        public void Execute (Action executeAction,
                             Action undoAction,
                             bool isPush = true)
        {
            var command = new ZeroArgumentCommand (executeAction, undoAction);
            Execute (command, isPush);
        }

        public  void Execute<T> (T value,
                                 Action<T> executeAction,
                                 Action<T> undoAction,
                                 bool isPush = true)
        {
            var command = new OneArgumentCommand<T> (value, executeAction, undoAction);
            Execute (command, isPush);
        }


        public  void ExecuteClear<T> (ICollection<T> collection,
                                      bool isPush = true)
        {
            var command = Command.CreateClear (collection);
            Execute (command, isPush);
        }

        public  void ExecuteDictionaryAdd<TKey,TValue> (IDictionary<TKey,TValue> dictionary,
                                                        TKey key,
                                                        TValue value,
                                                        bool isPush = true)
        {
            var command = Command.CreateDictionaryAdd (dictionary, key, value);
            Execute (command, isPush);
        }

        public  void ExecuteDictionaryRemove<TKey,TValue> (IDictionary<TKey,TValue> dictionary,
                                                           TKey key,
                                                           bool isPush = true)
        {
            var command = Command.CreateDictionaryRemove (dictionary, key);
            Execute (command, isPush);
        }

        public void ExecuteDictionaryReplace<TKey,TValue> (IDictionary<TKey,TValue> dictionary,
                                                           TKey key,
                                                           TValue value,
                                                           bool isPush = true)
        {
            var command = Command.CreateDictionaryReplace (dictionary, key, value);
            Execute (command, isPush);
        }

        public void ExecuteListInsert<T> (IList<T> list,
                                          int index,
                                          T value,
                                          bool isPush = true)
        {
            var command = Command.CreateListInsert (list, index, value);
            Execute (command, isPush);
        }

        public void ExecuteListAdd<T> (IList<T> list,
                                       T value,
                                       bool isPush = true)
        {
            var command = Command.CreateListAdd (list, value);
            Execute (command, isPush);
        }

        public void ExecuteListRemove<T> (IList<T> list,
                                          int index,
                                          bool isPush = true)
        {
            var command = Command.CreateListRemove (list, index);
            Execute (command, isPush);
        }

        public void ExecuteListRemove<T> (IList<T> list,
                                          T item,
                                          bool isPush = true)
        {
            var command = Command.CreateListRemove (list, item);
            Execute (command, isPush);
        }

        public void ExecuteListReplace<T> (IList<T> list,
                                           int index,
                                           T value,
                                           bool isPush = true)
        {
            var command = Command.CreateListReplace (list, index, value);
            Execute (command, isPush);
        }

        public void Execute<T> (T newValue,
                                T oldValue,
                                Action<T> action,
                                bool isPush = true)
        {
            var command = Command.Create (newValue, oldValue, action);
            Execute (command, isPush);
        }

        public void Execute<T1,T2> (T1 target,
                                    T2 newValue,
                                    T2 oldValue,
                                    Action<T1,T2> action,
                                    bool isPush = true)
        {
            var command = Command.Create (target, newValue, oldValue, action);
            Execute (command, isPush);
        }
    }
}

