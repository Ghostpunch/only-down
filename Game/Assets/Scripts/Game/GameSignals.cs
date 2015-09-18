using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Game.Views;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Game
{
    // Game Lifecycle
    public class GameStartedSignal : Signal { }

    /// <summary>
    /// The Signal that will be dispatched to setup the Level:
    /// Things like environment generation, enemy spawning, player
    /// placement, etc.
    /// </summary>
    public class SetupLevelSignal : Signal { }

    /// <summary>
    /// The Signal that will be dispatched after level setup has
    /// occured.
    /// </summary>
    public class LevelStartedSignal : Signal { }

    /// <summary>
    /// This signal is dispatched when the player attempts to dig
    /// </summary>
    public class PlayerDigSignal : Signal<Vector3> { }

    public class EnemyPlayerCollisionSignal : Signal<EnemyView> { }

    public class LevelScrollSignal : Signal<float, int> { }
}
