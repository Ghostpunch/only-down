using Ghostpunch.OnlyDown.Common.Views;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Game.Views
{
    public class EnemyView : ViewBase
    {
        public Signal _enemyTouch = new Signal();

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == Tags.Player)
            {
                _enemyTouch.Dispatch();
            }
        }
    }
}