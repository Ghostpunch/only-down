using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using System;
using Ghostpunch.OnlyDown.Menu.ViewModels;

namespace Ghostpunch.OnlyDown.Menu.Views
{
    public class GameOverView : View
    {
        public Button _restartButton;

        [Inject]
        public GameOverViewModel VM { get; set; }

        protected override void Start()
        {
            base.Start();

            if (_restartButton != null)
                _restartButton.onClick.AddListener(OnRestart);
        }

        private void OnRestart()
        {
            VM.RestartButtonPressedCommand.Execute(null);
        }
    }
}