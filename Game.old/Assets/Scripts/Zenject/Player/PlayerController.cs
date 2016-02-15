using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlyDown.Messaging;
using UnityEngine;
using Zenject;

namespace OnlyDown
{
    public class PlayerController : IFixedTickable, IInitializable
    {
        private enum PlayerStates
        {
            Waiting,
            Playing,
            Dead,
        }

        PlayerView _view;
        IMessageSystem _messageSystem;
        PlayerStates _state = PlayerStates.Waiting;
        Transform _transform = null;
        Vector3 _currentDirection = Vector3.left;
        Settings _settings = null;

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

        public PlayerController(PlayerView view, Settings settings, IMessageSystem messageSystem)
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
        }

        public void FixedTick()
        {
            if (_state == PlayerStates.Playing)
                UpdatePlaying();
        }

        private void UpdatePlaying()
        {
            _transform.Translate(_currentDirection * _settings.MoveSpeed * Time.deltaTime);
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
