﻿using Ghostpunch.OnlyDown.Common.ViewModels;
using Ghostpunch.OnlyDown.Game.Views;
using Lean;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Game.ViewModels
{
    public class PlayerViewModel : ViewModelBase<PlayerView>
    {
        [Inject]
        public PlayerDigSignal PlayerDig { get; set; }

        private Transform _transform = null;

        private Vector3 _currentDirection = Vector3.right;

        public override void OnRegister()
        {
            base.OnRegister();

            _transform = transform;
        }

        void OnEnable()
        {
            LeanTouch.OnFingerTap += OnTap;
        }

        void OnDisable()
        {
            LeanTouch.OnFingerTap -= OnTap;
        }

        private void OnTap(LeanFinger obj)
        {
            PlayerDig.Dispatch(_transform.localPosition);
        }

        void FixedUpdate()
        {
            _transform.Translate(_currentDirection * View._moveSpeed * Time.deltaTime);
        }

        void OnCollisionEnter(Collision collision)
        {
            _currentDirection *= -1f;
        }
    }
}
