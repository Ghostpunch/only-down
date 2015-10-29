using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostpunch.OnlyDown.Messaging
{
    public interface IMessageSystem
    {
        void Subscribe(string eventName, ICommand listener);
        void Unsubscribe(string eventName, ICommand listener);
        void Broadcast(string eventName, object args);
    }
}
