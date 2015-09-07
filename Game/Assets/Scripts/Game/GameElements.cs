using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostpunch.OnlyDown.Game
{
    public enum GameElement
    {
        EnemyPool,
        SandPool,
        Environment,     // Injection name of the GameObject that parents the enemies/sand/player/etc
        Player,         // Injection name of the player
    }
}
