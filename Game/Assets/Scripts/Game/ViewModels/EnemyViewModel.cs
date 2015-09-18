using UnityEngine;
using System.Collections;
using Ghostpunch.OnlyDown.Common.ViewModels;
using Ghostpunch.OnlyDown.Game.Views;
using System;
using strange.extensions.pool.api;

namespace Ghostpunch.OnlyDown.Game.ViewModels
{
    public class EnemyViewModel : ViewModelBase<EnemyView>
    {
        [Inject]
        public EnemyPlayerCollisionSignal EnemyPlayerCollision { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View._enemyTouch.AddListener(OnEnemyTouch);
        }

        private void OnEnemyTouch()
        {
            EnemyPlayerCollision.Dispatch(View);
        }
    }
}