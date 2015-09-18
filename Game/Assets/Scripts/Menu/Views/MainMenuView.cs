using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common;
using Ghostpunch.OnlyDown.Common.Views;
using Ghostpunch.OnlyDown.Menu.ViewModels;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Ghostpunch.OnlyDown.Menu.Views
{
    public class MainMenuView : View
    {
        public Button _startButton, _quitButton;

        [Inject]
        public MainMenuViewModel VM { get; set; }

        protected override void Start()
        {
            base.Start();

            if (_startButton != null)
                _startButton.onClick.AddListener(OnStartClick);

            if (_quitButton != null)
                _quitButton.onClick.AddListener(OnQuit);
        }

        private void OnStartClick()
        {
            VM.StartButtonPressedCommand.Execute(null);
        }

        private void OnQuit()
        {
            VM.QuitButtonPressedCommand.Execute(null);
        }
    }
}
