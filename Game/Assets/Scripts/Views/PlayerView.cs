using UnityEngine;
using Lean;

namespace Ghostpunch.OnlyDown
{
    public class PlayerView : BaseView<PlayerViewModel>
    {
        /// <summary>
        /// This is essentially a throw away model, for design purposes
        /// Values will be transfered to VM and never used again.
        /// If values are updated in the Inspector, the VM will be updated.
        /// But modifying these values at runtime through code will do nothing.
        /// </summary>
        public PlayerModel _settings;

        protected override void Awake()
        {
            base.Awake();

            ViewModel.Model = _settings;
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
            ViewModel.OnTap.Execute(null);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == Tags.Walls)
                ViewModel.OnWallHit.Execute(collision);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.Finish)
                ViewModel.OnScrollPointHit.Execute(other);
        }

        void OnValidate()
        {
            if (ViewModel != null && _settings != null)
                ViewModel.Model = _settings;
        }
    }
}
