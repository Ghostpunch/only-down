//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Ghostpunch.OnlyDown.Common;
//using Ghostpunch.OnlyDown.Game.Views;
//using strange.extensions.command.impl;
//using strange.extensions.pool.api;
//using UnityEngine;

//namespace Ghostpunch.OnlyDown.Game.Commands
//{
//    public class EnemyPlayerCollisionCommand : Command
//    {
//        [Inject(GameElement.Player)]
//        public GameObject Player { get; set; }

//        [Inject(GameElement.EnemyPool)]
//        public IPool<GameObject> EnemyPool { get; set; }

//        [Inject]
//        public EnemyView Enemy { get; set; }

//        [Inject]
//        public GameOverSignal GameOver { get; set; }

//        private static Vector3 GTFO_POS = new Vector3(1000f, 0, 1000f);

//        public override void Execute()
//        {
//            Enemy.gameObject.SetActive(false);
//            Enemy.transform.localPosition = GTFO_POS;
//            EnemyPool.ReturnInstance(Enemy);

//            Player.SetActive(false);

//            GameOver.Dispatch();
//        }
//    }
//}
