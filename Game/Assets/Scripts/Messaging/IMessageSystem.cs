using Ghostpunch.OnlyDown.Command;

namespace Ghostpunch.OnlyDown.Messaging
{
    public interface IMessageSystem
    {
        void Subscribe(string eventName, ICommand listener);
        void Unsubscribe(string eventName, ICommand listener);
        void Broadcast(string eventName, object args);
    }
}
