using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Messaging;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    public class PowerUpManagerViewModel : ObservableObject
    {
        private Transform _transform = null;
        private Camera _mainCamera = null;

        private List<Transform> _itemParents = new List<Transform>();

        private PowerUpModel _model = null;
        public PowerUpModel Model
        {
            get { return _model; }
            set { Set(() => Model, ref _model, value); }
        }

        void Start()
        {
            _transform = transform;
            _mainCamera = Camera.main ?? Camera.allCameras[0];
        }

        void OnEnable()
        {
            var messageSystem = MessageSystem.Default;
            messageSystem.Subscribe<PlayerHitScrollPointMessage>(OnScroll);
        }

        void OnDisable()
        {
            var messageSystem = MessageSystem.Default;
            messageSystem.Unsubscribe<PlayerHitScrollPointMessage>(OnScroll);
        }

        private void OnScroll(PlayerHitScrollPointMessage obj)
        {
        }

        private SpriteRenderer CreateNewItem()
        {
            return null;
        }
    }
}
