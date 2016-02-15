using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Command;

namespace Ghostpunch.OnlyDown.Messaging
{
    public class MessageSystem : IMessageSystem
    {
        #region Singleton
        private static readonly object CreationLock = new object();
        private static IMessageSystem _defaultInstance = null;

        public static IMessageSystem Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    lock (CreationLock)
                    {
                        // This double check done because another thread could've 
                        // created this while we were waiting.
                        if (_defaultInstance == null)
                            _defaultInstance = new MessageSystem();
                    }
                }

                return _defaultInstance;
            }
        }
        #endregion

        private readonly object _registerLock = new object();
        private Dictionary<Type, List<WeakAction>> _messageDictionary = new Dictionary<Type, List<WeakAction>>();

        public void Subscribe<TMessage>(Action<TMessage> listener) where TMessage : MessageBase
        {
            var type = typeof(TMessage);

            lock (_registerLock)
            {
                if (_messageDictionary.ContainsKey(type))
                    _messageDictionary[type].Add(new WeakAction<TMessage>(listener));
                else
                {
                    var newActionList = new List<WeakAction>();
                    newActionList.Add(new WeakAction<TMessage>(listener));
                    _messageDictionary.Add(type, newActionList);
                }
            }
        }

        public void Unsubscribe<TMessage>(Action<TMessage> listener) where TMessage : MessageBase
        {
            var type = typeof(TMessage);

            lock (_registerLock)
            {
                if (_messageDictionary.ContainsKey(type))
                {
                    var list = _messageDictionary[type];
                    var weakAction = new WeakAction<TMessage>(listener);
                    list.Remove(weakAction);
                }
            }
        }

        public void Broadcast<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var type = typeof(TMessage);
            if (_messageDictionary.ContainsKey(type))
            {
                var list = _messageDictionary[type];
                var count = list.Count;
                for (int i = 0; i < count; ++i)
                    (list[i] as WeakAction<TMessage>).Execute(message);
            }
        }
    }
}
