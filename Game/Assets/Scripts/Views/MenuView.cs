using UnityEngine;
using System.Collections;
using Zenject;
using Ghostpunch.OnlyDown.Messaging;

namespace Ghostpunch.OnlyDown
{
    public class MenuView : MonoBehaviour
    {
        private IMessageSystem _messageSystem = null;

        [PostInject]
        public void Initialize(IMessageSystem messageSystem)
        {
            _messageSystem = messageSystem;
        }

        public void OnButtonPress(string message)
        {
            Debug.Log("Firing message: " + message);
            //_messageSystem.Broadcast(message, null);
        }
    }
}
