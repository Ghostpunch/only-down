using System;
using Ghostpunch.OnlyDown.Command;

namespace Ghostpunch.OnlyDown.Messaging
{
    public interface IMessageSystem
    {
        void Subscribe<TMessage>(Action<TMessage> listener) where TMessage : MessageBase;
        void Unsubscribe<TMessage>(Action<TMessage> listener) where TMessage : MessageBase;
        void Broadcast<TMessage>(TMessage message) where TMessage : MessageBase;
    }
}
