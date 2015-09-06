using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine.UI;

namespace Ghostpunch.OnlyDown.Common.Views
{
    public class ButtonView : ViewBase
    {
        public Signal _clickSignal = new Signal();

        internal override void Initialize()
        {
            base.Initialize();

            // Add code to grab the GUI 4.6 button component
            // and link it to the OnClick method.
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _clickSignal.Dispatch();
        }
    }
}
