using UnityEngine;
using System.Collections;
using Ghostpunch.OnlyDown.Messaging;
using Zenject;
using System;

namespace Ghostpunch.OnlyDown
{
    public class MainMenuPresenter : IInitializable
    {
        private MenuView _view = null;
        private IMessageSystem _messageSystem = null;

        private RelayCommand _onStartGame = null;
        public RelayCommand OnStartGame
        {
            get
            {
                return _onStartGame ?? (_onStartGame = new RelayCommand(() =>
                {
                    _view.gameObject.SetActive(false);
                }));
            }
        }

        public MainMenuPresenter(MenuView view, IMessageSystem messageSystem)
        {
            _view = view;
            _messageSystem = messageSystem;
        }

        public void Initialize()
        {
            _messageSystem.Subscribe(GameMessages.StartGameButton, OnStartGame);
        }
    }
}
