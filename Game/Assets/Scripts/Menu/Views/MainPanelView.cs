using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common.Views;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Menu.Views
{
    public class MainPanelView : ViewBase
    {
        [SerializeField]
        private ButtonView _startButton = null;

        internal Signal _startSignal = new Signal();

        internal override void Initialize()
        {
            base.Initialize();

            if (_startButton != null)
                _startButton._clickSignal.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            _startSignal.Dispatch();
        }
    }
}
