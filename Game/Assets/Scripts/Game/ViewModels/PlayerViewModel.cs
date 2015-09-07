﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common;
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
    }
}
