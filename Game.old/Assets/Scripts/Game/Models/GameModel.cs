using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostpunch.OnlyDown.Game.Models
{
    public class GameModel : IGameModel
    {
        public int Score { get; set; }

        public void Reset()
        {
            Score = 0;
        }
    }
}
