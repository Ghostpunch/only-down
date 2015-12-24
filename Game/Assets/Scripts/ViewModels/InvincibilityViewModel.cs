using System;
using Ghostpunch.OnlyDown.Command;

namespace Ghostpunch.OnlyDown
{
    public class InvincibilityViewModel : PowerUpViewModel
    {
        protected override void AffectPlayer(PlayerViewModel player)
        {
            var invincibilityHandler = player as IInvincibilityHandler;

            if (invincibilityHandler != null)
            {
                invincibilityHandler.HandleInvincibility(5f);
            }
        }
    }
}