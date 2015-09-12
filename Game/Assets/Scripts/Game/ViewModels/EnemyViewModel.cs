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
        [Inject(GameElement.Player)]
        public GameObject Player { get; set; }

        [Inject(GameElement.EnemyPool)]
        public IPool<GameObject> EnemyPool { get; set; }

        private static Vector3 GTFO_POS = new Vector3(1000f, 0, 1000f);

        public override void OnRegister()
        {
            base.OnRegister();

            View._enemyTouch.AddListener(OnEnemyTouch);
        }

        private void OnEnemyTouch()
        {
            gameObject.SetActive(false);
            gameObject.transform.localPosition = GTFO_POS;
            EnemyPool.ReturnInstance(gameObject);

            Player.SetActive(false);
        }
    }
}