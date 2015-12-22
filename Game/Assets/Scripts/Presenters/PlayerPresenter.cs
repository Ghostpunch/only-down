using UnityEngine;
using System.Collections;
using Zenject;
using System;
using Ghostpunch.OnlyDown.Messaging;

namespace Ghostpunch.OnlyDown
{
    public class PlayerPresenter : ObservableObject, IInitializable, IFixedTickable
    {
        private enum PlayerStates
        {
            Waiting,
            Playing,
            Dead,
        }

        #region Fields and Properties

        private PlayerView _view = null;
        private IMessageSystem _messageSystem = null;
        private Settings _settings = null;

        private Transform _transform = null;
        private Vector3 _currentDirection = Vector3.left;
        private PlayerStates _state = PlayerStates.Waiting;

        #endregion

        #region Commands

        private RelayCommand<Collision> _onWallHit;
        private RelayCommand<Collision> OnWallHit
        {
            get
            {
                return _onWallHit ?? (_onWallHit = new RelayCommand<Collision>(collision =>
                {
                    _currentDirection *= -1;
                }));
            }
        }

        private RelayCommand _onPlayerTap;
        private RelayCommand OnPlayerTap
        {
            get
            {
                return _onPlayerTap ?? (_onPlayerTap = new RelayCommand(() =>
                {

                }));
            }
        }

        private RelayCommand _onGameStart;
        private RelayCommand OnGameStart
        {
            get
            {
                return _onGameStart ?? (_onGameStart = new RelayCommand(() =>
                {
                    Debug.Log("Switching states to Playing");
                    _state = PlayerStates.Playing;
                }));
            }
        }

        #endregion

        public PlayerPresenter(PlayerView view, Settings settings, IMessageSystem messageSystem)
        {
            _view = view;
            _settings = settings;
            _messageSystem = messageSystem;
        }

        public void Initialize()
        {
            _transform = _view.transform;

            _messageSystem.Subscribe(GameMessages.PlayerHitWall, OnWallHit);
            _messageSystem.Subscribe(GameMessages.PlayerTap, OnPlayerTap);
            _messageSystem.Subscribe(GameMessages.StartGameButton, OnGameStart);
        }

        public void FixedTick()
        {
            if (_state == PlayerStates.Playing)
            {
                _transform.Translate(_currentDirection * _settings.MoveSpeed * Time.deltaTime);
                Debug.Log("I should be going!");
            }
        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            private float _moveSpeed = 5f;
            [SerializeField]
            private float _fallSpeed = 5f;

            public float MoveSpeed { get { return _moveSpeed; } }
            public float FallSpeed { get { return _fallSpeed; } }
        }
    }
}
