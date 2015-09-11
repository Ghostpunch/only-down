using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common.Views;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Game.Views
{
    public class PlayerView : ViewBase
    {
        public float _moveSpeed = 1f;

        public Signal _dig = new Signal();

        private Transform _transform = null;
        private Vector3 _currentDirection = Vector3.right;
        private bool _isRunning = true;


        internal override void Initialize()
        {
            base.Initialize();

            _transform = transform;

            _dig.AddListener(AnimateDig);
        }

        void FixedUpdate()
        {
            if (_isRunning)
                _transform.Translate(_currentDirection * _moveSpeed * Time.deltaTime);
        }

        void OnCollisionEnter(Collision collision)
        {
            _currentDirection *= -1f;
        }

        private void AnimateDig()
        {
            StartCoroutine(AnimateDigCR(.20f));
        }

        private IEnumerator AnimateDigCR(float animationLength)
        {
            _isRunning = false;
            var isAnimating = true;
            var elapsedTime = 0f;

            while (isAnimating && elapsedTime < animationLength)
            {
                yield return new WaitForEndOfFrame();

                _transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
            }

            _isRunning = true;
        }
    }
}
