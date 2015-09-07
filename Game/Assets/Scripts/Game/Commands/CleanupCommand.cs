using System;
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
        [Inject(GameElement.Environment)]
        public GameObject GameField { get; set; }

        [Inject(GameElement.SandPool)]
        public IPool<GameObject> SandPool { get; set; }

        public override void Execute()
        {
        }
    }
}
