using UnityEngine;
using Lean;
using System.ComponentModel;

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
            ViewModel.PropertyChanged += PropertyChanged;
        }

        void OnDisable()
        {
            LeanTouch.OnFingerTap -= OnFingerTap;
            ViewModel.PropertyChanged -= PropertyChanged;
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

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var isDiggingPropertyName = ObservableObject.GetPropertyName(() => ViewModel.IsDigging);

            if (e.PropertyName == isDiggingPropertyName)
            {
                // probably switch to a digging animation
            }
        }

        void OnValidate()
        {
            if (ViewModel != null && _settings != null)
                ViewModel.Model = _settings;
        }
    }
}
