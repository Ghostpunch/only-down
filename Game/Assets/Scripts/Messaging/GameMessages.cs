using UnityEngine;
using System.Collections;
using System;
using Ghostpunch.OnlyDown.Messaging;

namespace Ghostpunch.OnlyDown
{
    //public class GameMessages
    //{
    //    public const string PlayerHitWall = "PlayerHitWall";
    //    public const string PlayerHitEnemy = "PlayerHitEnemy";
    //    public const string PlayerTap = "PlayerTap";
    //    public const string PlayerHitScrollPoint = "PlayerHitScrollPoint";

    //    public const string StartGameButton = "StartGameButton";
    //    public const string RestartGameButton = "RestartGameButton";
    //}

    public class PlayerHitWall : MessageBase { }
    public class PlayerHitEnemy : MessageBase { }
    public class PlayerTap : MessageBase { }
    public class PlayerHitScrollPoint : MessageBase { }
    public class StartGameButton : MessageBase { }
    public class RestartGameButton : MessageBase { }
}
