using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.command.impl;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Menu.Commands
{
    public class GameStartCommand : Command
    {
        public override void Execute()
        {
            Application.LoadLevel("MainGame");
        }
    }
}
