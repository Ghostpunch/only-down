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
        public UIButton _restartButton;

        [Inject]
        public GameOverViewModel VM { get; set; }

        protected override void Start()
        {
            base.Start();

            if (_restartButton != null)
            {
                var onRestartDelegate = new EventDelegate(OnRestart);
                _restartButton.onClick.Add(onRestartDelegate);
            }
        }

        private void OnRestart()
        {
            VM.RestartButtonPressedCommand.Execute(null);
        }
    }
}