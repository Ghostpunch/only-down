using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Messaging;

namespace Ghostpunch.OnlyDown
{
    public abstract class PowerUpViewModel : ObservableObject
    {
        void OnEnable()
        {
            var messageSystem = MessageSystem.Default;
            messageSystem.Subscribe<PlayerPickUpItemMessage>(OnPlayerPickedUp);
        }

        void OnDisable()
        {
            var messageSystem = MessageSystem.Default;
            messageSystem.Unsubscribe<PlayerPickUpItemMessage>(OnPlayerPickedUp);
        }

        private void OnPlayerPickedUp(PlayerPickUpItemMessage obj)
        {
            if (obj.ItemPickedUp == gameObject)
            {
                AffectPlayer(obj.Player);
                Destroy(gameObject);
            }
        }

        protected abstract void AffectPlayer(PlayerViewModel player);
    }
}
