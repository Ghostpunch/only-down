using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostpunch.OnlyDown.Game.Models
{
    public interface IGameModel
    {
        void Reset();

        int Score { get; set; }
    }
}
