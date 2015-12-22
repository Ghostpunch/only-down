using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Command;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    public class PlayerViewModel : ObservableObject
    {
        private enum PlayerStates
        {
            Waiting,
            Playing,
            Dead,
        }

        #region Fields and Properties

        private PlayerModel _model = null;

        private Transform _transform = null;
        private Vector3 _currentDirection = Vector3.left;
        private PlayerStates _state = PlayerStates.Playing;

        public PlayerModel Model
        {
            get { return _model; }
            set { Set(() => Model, ref _model, value); }
        }
        #endregion

        #region Commands

        private RelayCommand<Collision> _onWallHit;
        public RelayCommand<Collision> OnWallHit
        {
            get
            {
                return _onWallHit ?? (_onWallHit = new RelayCommand<Collision>(collision =>
                {
                    _currentDirection *= -1;
                    Debug.Log("Player should turn around. Is it? " + _currentDirection);
                }));
            }
        }

        private RelayCommand<Collider> _onScrollPointHit;
        public RelayCommand<Collider> OnScrollPointHit
        {
            get
            {
                return _onScrollPointHit ?? (_onScrollPointHit = new RelayCommand<Collider>(collider =>
                {

                }));
            }
        }

        private RelayCommand _onTap;
        public RelayCommand OnTap
        {
            get
            {
                return _onTap ?? (_onTap = new RelayCommand(() =>
                {

                }));
            }
        }

        private RelayCommand _onGameStart;
        public RelayCommand OnGameStart
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

        void Start()
        {
            _transform = transform;
        }

        void FixedUpdate()
        {
            if (_state == PlayerStates.Playing)
            {
                _transform.Translate(_currentDirection * Model.MoveSpeed * Time.deltaTime);
                Debug.Log("I should be going!");
            }
        }
    }
}
