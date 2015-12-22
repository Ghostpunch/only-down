using Ghostpunch.OnlyDown.Command;
using Ghostpunch.OnlyDown.Messaging;
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

        private Transform _transform = null;
        private Vector3 _currentDirection = Vector3.left;
        private PlayerStates _state = PlayerStates.Waiting;

        private PlayerModel _model = null;
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
                    MessageSystem.Default.Broadcast(new PlayerDigMessage());
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

        void FixedUpdate()
        {
            if (_state == PlayerStates.Playing)
            {
                _transform.Translate(_currentDirection * Model.MoveSpeed * Time.deltaTime);
            }
        }

        #endregion

        private void OnGameStart(GameStartMessage obj)
        {
            Debug.Log("Switching states to Playing");
            _state = PlayerStates.Playing;
        }
    }
}
