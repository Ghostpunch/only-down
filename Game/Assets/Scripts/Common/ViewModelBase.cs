using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common.Views;
using strange.extensions.mediation.impl;

namespace Ghostpunch.OnlyDown.Common
{
    public abstract class ViewModelBase<V> : Mediator where V : ViewBase
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
