using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common.Signals;
using strange.extensions.command.impl;

namespace Ghostpunch.OnlyDown.Game.Commands
{
    public class GameModuleStartCommand : Command
    {
        [Inject]
        public GameStartSignal GameStart { get; set; }

        public override void Execute()
        {
            // I might want some debug data to be initialized here

            // For now I'm just going to kick start the game
            GameStart.Dispatch();
        }
    }
}
