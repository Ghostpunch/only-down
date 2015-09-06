using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.signal.impl;

namespace Ghostpunch.OnlyDown.Common.Signals
{
    public class StartSignal : Signal { }

    //Input
    public class GameInputSignal : Signal<int> { };

    //Game
    public class GameStartSignal : Signal { }
    public class GameEndSignal : Signal { }
    public class LevelStartSignal : Signal { }
    public class LevelEndSignal : Signal { }
    public class UpdateLivesSignal : Signal<int> { }
    public class UpdateScoreSignal : Signal<int> { }
    public class UpdateLevelSignal : Signal<int> { }
}
