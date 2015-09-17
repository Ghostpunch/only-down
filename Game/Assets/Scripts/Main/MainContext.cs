using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using Ghostpunch.OnlyDown.Common;
using strange.extensions.context.api;
using Ghostpunch.OnlyDown.Common.Signals;

namespace Ghostpunch.OnlyDown.Main
{
    public class MainContext : SignalContext
    {
        public MainContext(MonoBehaviour view) : base(view) { }
        public MainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags) { }

        protected override void mapBindings()
        {
            base.mapBindings();

            if (Context.firstContext == this)
            {

            }

            injectionBinder.Bind<StartSignal>().To<MainStartupCommand>();
        }
    }
}
