using System;
using System.Collections;
using Ghostpunch.OnlyDown.Common;
using Ghostpunch.OnlyDown.Common.Commands;
using Ghostpunch.OnlyDown.Common.ViewModels;
using Ghostpunch.OnlyDown.Common.Views;
using Ghostpunch.OnlyDown.Menu.Views;
using strange.extensions.context.api;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Menu.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject ContextView { get; set; }

        [Inject]
        public GameStartSignal GameStart { get; set; }

        [Inject]
        public GameOverSignal GameOver { get; set; }

        private bool _isMainMenuVisible = true;
        public bool IsMainMenuVisible
        {
            get { return _isMainMenuVisible; }
            set
            {
                Set(() => IsMainMenuVisible, ref _isMainMenuVisible, value);
            }
        }

        private bool _isGameOverVisible = false;
        public bool IsGameOverVisible
        {
            get { return _isGameOverVisible; }
            set
            {
                Set(() => IsGameOverVisible, ref _isGameOverVisible, value);
            }
        }

        private float _mainMenuAlpha;
        public float MainMenuAlpha
        {
            get { return _mainMenuAlpha; }
            set
            {
                Set(() => MainMenuAlpha, ref _mainMenuAlpha, value);
            }
        }

        [PostConstruct]
        public void OnCreation()
        {
            Debug.Log("MenuViewModel:Start");
            GameStart.AddListener(OnGameStart);
            GameOver.AddListener(OnGameOver);
        }

        [Deconstruct]
        public void OnDestruction()
        {
            Debug.Log("MenuViewModel:OnDestroy");
            GameStart.RemoveListener(OnGameStart);
            GameOver.RemoveListener(OnGameOver);
        }

        private void OnGameStart()
        {
            Debug.Log("MenuViewModel:OnGameStart");
            IsMainMenuVisible = false;
        }

        private void OnGameOver()
        {
            Debug.Log("MenuViewModel:OnGameOver");
            IsGameOverVisible = true;
        }
    }
}
