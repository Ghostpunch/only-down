using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostpunch.OnlyDown.Game.Models
{
    public interface IGameConfig
    {
        int GridCellSize { get; set; }

        int GridWidth { get; set; }

        int GridHeight { get; set; }
    }
}
