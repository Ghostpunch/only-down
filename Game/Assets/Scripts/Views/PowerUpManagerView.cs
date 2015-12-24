using UnityEngine;
using System.Collections;

namespace Ghostpunch.OnlyDown
{
    public class PowerUpManagerView : BaseView<PowerUpManagerViewModel>
    {
        public PowerUpModel _settings = null;

        protected override void Awake()
        {
            base.Awake();

            ViewModel.Model = _settings;
        }

        void OnValidate()
        {
            if (ViewModel != null && _settings != null)
                ViewModel.Model = _settings;
        }
    }
}
