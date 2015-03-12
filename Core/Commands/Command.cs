using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Cession.Commands
{
    public static class CollectionExtension
    {
        public static void AddRange<T> (this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add (item);
        }
    }

    public abstract class Command
    {
        protected Command ()
        {
        }

        public abstract void Execute ();

        public abstract void Undo ();

        public object Tag{ get; set; }

        #region "utility methods for helping create commands"

        public static Command CreateClear<T> (ICollection<T> collection)
        {
            var command = new OneArgumentCommand<IEnumerable<T>> (collection.ToArray (),
                              collection.Clear,
                              collection.AddRange);
            return command;
        }

        public static Command CreateDictionaryAdd<TKey,TValue> (IDictionary<TKey,TValue> dictionary,
                                                                TKey key,
                                                                TValue value)
        {
            var command = new TwoArgumentCommand<TKey,TValue> (key,
                              value,
                              dictionary.Add,
                              (k, v) => dictionary.Remove (k));
            return command;
        }

        public static Command CreateDictionaryRemove<TKey,TValue> (IDictionary<TKey,TValue> dictionary,
                                                                   TKey key)
        {
            var command = new TwoArgumentCommand<TKey,TValue> (key,
                              dictionary [key],
                              (k, v) => dictionary.Remove (k),
                              dictionary.Add);
            return command;
        }

        public static Command CreateDictionaryReplace<TKey,TValue> (IDictionary<TKey,TValue> dictionary,
                                                                    TKey key,
                                                                    TValue value)
        {
            return Create<IDictionary<TKey,TValue>,TKey,TValue> (dictionary, 
                key, 
                value, 
                dictionary [key],
                (d, k, v) => d [k] = v);
        }

        public static Command CreateListInsert<T> (IList<T> list, int index, T value)
        {
            var command = new TwoArgumentCommand<int,T> (index, value,
                              list.Insert,
                              list.RemoveAt);
            return command;
        }

        public static Command CreateListAdd<T> (IList<T> list, T value)
        {
            return CreateListInsert (list, list.Count, value);
        }

        public static Command CreateListRemove<T> (IList<T> list, int index)
        {
            return new TwoArgumentCommand<int,T> (index,
                list [index],      
                list.RemoveAt,
                list.Insert);
        }

        public static Command CreateListRemove<T> (IList<T> list, T item)
        {
            return CreateListRemove (list, list.IndexOf (item));
        }

        public static Command CreateListReplace<T> (IList<T> list, int index, T value)
        {
            return Create<IList<T>,int,T> (list, index, value, list [index],
                (l, i, v) => l [i] = v);
        }

        public static Command Create<T> (T newValue, T oldValue, Action<T> action)
        {
            return new TwoArgumentCommand<T,T> (newValue, oldValue,
                (nv, ov) => action.Invoke (nv),
                (nv, ov) => action.Invoke (ov));
        }

        public static Command Create<T1,T2> (T1 target, T2 newValue, T2 oldValue, Action<T1,T2> action)
        {
            return new ThreeArgumentCommand<T1,T2,T2> (target, newValue, oldValue,
                (t, nv, ov) => action.Invoke (t, nv),
                (t, nv, ov) => action.Invoke (t, ov));
        }

        public static Command Create<T1,T2,T3> (T1 target,
                                                T2 index,
                                                T3 newValue,
                                                T3 oldValue,
                                                Action<T1,T2,T3> action)
        {
            return new FourArgumentCommand<T1,T2,T3,T3> (target, index, newValue, oldValue,
                (l, i, nv, ov) => action.Invoke (target, i, nv),
                (l, i, nv, ov) => action.Invoke (target, i, ov));
        }

        public static Command CreateSetProperty<T1,T2> (T1 target,
                                                       T2 value,
                                                       string propertyName)
        {
            Type type = typeof(T1);
            PropertyInfo pi = type.GetProperty (propertyName, BindingFlags.SetProperty |
                              BindingFlags.GetProperty |
                              BindingFlags.Public |
                              BindingFlags.Instance);
            if (null == pi)
            {
                string message = string.Format ("can't find property {0} on type {1} or this property is readonly", 
                                     propertyName, 
                                     type.Name);
                throw new ArgumentException (message);
            }

            var oldValue = (T2)pi.GetValue (target, null);
            var action = Delegate.CreateDelegate (typeof(Action<T2>), target, pi.SetMethod) as Action<T2>;
            return Create (value, oldValue, action);
        }

        #endregion

    }
}

