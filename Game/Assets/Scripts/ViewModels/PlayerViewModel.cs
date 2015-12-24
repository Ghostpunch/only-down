using System;
using System.Collections;
using Ghostpunch.OnlyDown.Command;
using Ghostpunch.OnlyDown.Messaging;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    public class PlayerViewModel : ObservableObject, IInvincibilityHandler
    {
        private enum PlayerStates
        {
            Waiting,
            Playing,
            Dead,
        }

        #region Fields and Properties

        private Transform _transform = null;
        private Vector3 _currentDirection = Vector3.left;
        private PlayerStates _state = PlayerStates.Waiting;

        private PlayerModel _model = null;
        public PlayerModel Model
        {
            get { return _model; }
            set { Set(() => Model, ref _model, value); }
        }

        private bool _isDigging = false;
        public bool IsDigging
        {
            get { return _isDigging; }
            set { Set(() => IsDigging, ref _isDigging, value); }
        }

        private bool _isInvincible = false;
        public bool IsInvincible
        {
            get { return _isInvincible; }
            set { Set(() => IsInvincible, ref _isInvincible, value); }
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

        private RelayCommand<GameObject> _onPickUpItem;
        public RelayCommand<GameObject> OnPickUpItem
        {
            get
            {
                return _onPickUpItem ?? (_onPickUpItem = new RelayCommand<GameObject>(item =>
                {
                    var messageSystem = MessageSystem.Default;
                    messageSystem.Broadcast(new PlayerPickUpItemMessage
                    {
                        Player = this,
                        ItemPickedUp = item
                    });
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
                    StartCoroutine(DigOneLevel());
                    MessageSystem.Default.Broadcast(new PlayerDigMessage
                    {
                        PlayerPosition = _transform.position
                    });
                }));
            }
        }

        #endregion

        #region Unity Lifecycle

        void Start()
        {
            _transform = transform;
        }

        void OnEnable()
        {
            var messageSystem = MessageSystem.Default;
            messageSystem.Subscribe<GameStartMessage>(OnGameStart);
        }

        void OnDisable()
        {
            var messageSystem = MessageSystem.Default;
            messageSystem.Unsubscribe<GameStartMessage>(OnGameStart);
        }

        void Update()
        {
            if (_state == PlayerStates.Playing && !IsDigging)
            {
                _transform.Translate(_currentDirection * Model.MoveSpeed * Time.deltaTime);
            }
        }

        #endregion

        private void OnGameStart(GameStartMessage obj)
        {
            StartCoroutine(DigOneLevel());
            _state = PlayerStates.Playing;
        }

        private IEnumerator DigOneLevel()
        {
            IsDigging = true;

            var startingPosition = _transform.localPosition;
            var targetPosition = startingPosition + Vector3.down;
            var elapsedTime = 0f;
            var normalizedTime = 1f / Model.FallTime;

            while (elapsedTime < Model.FallTime)
            {
                var position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime * normalizedTime);

                _transform.localPosition = position;

                yield return new WaitForEndOfFrame();

                elapsedTime += Time.deltaTime;
            }

            IsDigging = false;
        }

        #region IInvinsibilityHandler Implementation

        public void HandleInvincibility(float time)
        {
            StartCoroutine(HandleInvincibilityCR(time));
        }

        private IEnumerator HandleInvincibilityCR(float time)
        {
            IsInvincible = true;

            yield return new WaitForSeconds(time);

            IsInvincible = false;
        }

        #endregion
    }
}
