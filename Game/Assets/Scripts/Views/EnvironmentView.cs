using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Ghostpunch.OnlyDown
{
    public class EnvironmentView : BaseView<EnvironmentViewModel>
    {
        /// <summary>
        /// This is essentially a throw away model, for design purposes
        /// Values will be transfered to VM and never used again.
        /// If values are updated in the Inspector, the VM will be updated.
        /// But modifying these values at runtime through code will do nothing.
        /// </summary>
        public EnvironmentModel _settings = null;

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
