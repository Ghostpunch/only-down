using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common.Signals;
using Ghostpunch.OnlyDown.Common.Views;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Menu.Views
{
    public class MainPanelMediator : ViewMediatorBase<MainPanelView>
    {
        [Inject]
        public GameStartSignal GameStartSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View._startSignal.AddListener(OnStartGame);
        }

        private void OnStartGame()
        {
            GameStartSignal.Dispatch();
        }
    }
}
