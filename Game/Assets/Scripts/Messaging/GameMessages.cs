using Ghostpunch.OnlyDown.Messaging;

namespace Ghostpunch.OnlyDown
{
    public class GameStartMessage : MessageBase { }
    public class GameRestartMessage : MessageBase { }
    public class PlayerHitWallMessage : MessageBase { }
    public class PlayerHitEnemyMessage : MessageBase { }
    public class PlayerDigMessage : MessageBase { }
    public class PlayerHitScrollPointMessage : MessageBase { }
}
