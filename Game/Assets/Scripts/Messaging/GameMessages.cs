using Ghostpunch.OnlyDown.Messaging;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    public class GameStartMessage : MessageBase { }
    public class GameRestartMessage : MessageBase { }
    public class PlayerHitWallMessage : MessageBase { }
    public class PlayerHitEnemyMessage : MessageBase { }
    public class PlayerDigMessage : MessageBase
    {
        public Vector3 PlayerPosition { get; set; }
    }
    public class PlayerHitScrollPointMessage : MessageBase { }
}
