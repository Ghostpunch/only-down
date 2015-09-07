using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostpunch.OnlyDown.Game.Models
{
    public class GameConfig : IGameConfig
    {
        public int GridCellSize { get; set; }

        public int GridHeight { get; set; }

        public int GridWidth { get; set; }

        [PostConstruct]
        public void PostConstruct()
        {
            GridCellSize = 1;

            GridHeight = 11;

            GridWidth = 11;
        }
    }
}
