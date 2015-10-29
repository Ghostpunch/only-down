using UnityEngine;
using System.Collections;
using Ghostpunch.OnlyDown.Messaging;
using Zenject;
using Lean;

namespace Ghostpunch.OnlyDown
{
    public class PlayerView : MonoBehaviour
    {
        private IMessageSystem _messageSystem = null;

        [PostInject]
        public void Initialize(IMessageSystem messageSystem)
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
            if (collision.gameObject.tag == Tags.Walls)
                _messageSystem.Broadcast(GameMessages.PlayerHitWall, collision);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.Finish)
                _messageSystem.Broadcast(GameMessages.PlayerHitScrollPoint, other);
        }
    }
}
