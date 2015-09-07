using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.command.impl;

namespace Ghostpunch.OnlyDown.Game.Commands
{
    public class StartLevelCommand : Command
    {
        [Inject]
        public LevelStartedSignal LevelStarted { get; set; }

        public override void Execute()
        {
            LevelStarted.Dispatch();
        }
    }
}
