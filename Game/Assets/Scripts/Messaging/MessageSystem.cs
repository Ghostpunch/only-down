using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Command;

namespace Ghostpunch.OnlyDown.Messaging
{
    public class MessageSystem : IMessageSystem
    {
        private Dictionary<string, Action<object>> _messageDictionary = new Dictionary<string, Action<object>>();

        public void Broadcast(string eventName, object args)
        {
            Action<object> existingAction = null;
            if (_messageDictionary.TryGetValue(eventName, out existingAction))
                existingAction(args);
        }

        public void Subscribe(string eventName, ICommand listener)
        {
            Action<object> existingAction = null;
            if (_messageDictionary.TryGetValue(eventName, out existingAction))
                existingAction += listener.Execute;
            else
            {
                existingAction = listener.Execute;
                _messageDictionary.Add(eventName, existingAction);
            }
        }

        public void Unsubscribe(string eventName, ICommand listener)
        {
            Action<object> existingAction = null;
            if (_messageDictionary.TryGetValue(eventName, out existingAction))
                existingAction -= listener.Execute;
        }
    }
}
