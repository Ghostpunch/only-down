using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.mediation.impl;

namespace Ghostpunch.OnlyDown.Common.Views
{
    public abstract class ViewMediatorBase<V> : Mediator where V : ViewBase
    {
        [Inject]
        public V View { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View.Initialize();
        }
    }
}
