using UnityEngine;
using System.Collections;
using Lean;
using System;
using OnlyDown.Messaging;
using Zenject;

namespace OnlyDown
{
    public class PlayerView : MonoBehaviour
    {
        IMessageSystem _messageSystem = null;

        [PostInject]
        public void Init(IMessageSystem messageSystem)
        {
            _messageSystem = messageSystem;
        }

        void OnEnable()
        {
            LeanTouch.OnFingerTap += OnFingerTap;
        }

        void OnDisable()
        {
            LeanTouch.OnFingerTap -= OnFingerTap;
        }

        private void OnFingerTap(LeanFinger finger)
        {
            _messageSystem.Broadcast(GameMessages.PlayerTap, null);
        }

        void OnCollisionEnter(Collision collision)
        {
            _messageSystem.Broadcast(GameMessages.PlayerHitWall, collision);
        }
    }
}
