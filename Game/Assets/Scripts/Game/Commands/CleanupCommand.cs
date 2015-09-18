﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Game.Commands
{
    public class CleanupCommand : Command
    {
        //[Inject(GameElement.Environment)]
        //public GameObject GameField { get; set; }

        [Inject(GameElement.SandPool)]
        public IPool<GameObject> SandPool { get; set; }

        public override void Execute()
        {
            GameObject gameField = null;

            if (injectionBinder.GetBinding<GameObject>(GameElement.Environment) != null)
                gameField = injectionBinder.GetInstance<GameObject>(GameElement.Environment);

            if (gameField != null)
                GameObject.Destroy(gameField);
        }
    }
}
