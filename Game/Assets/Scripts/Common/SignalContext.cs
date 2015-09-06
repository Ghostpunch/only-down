using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using Ghostpunch.OnlyDown.Common.Signals;

namespace Ghostpunch.OnlyDown.Common
{
    public class SignalContext : MVCSContext
    {
        public SignalContext(MonoBehaviour contextView) : base(contextView)
        {
        }

        public SignalContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();

            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        public override void Launch()
        {
            base.Launch();

            var startSignal = injectionBinder.GetInstance<StartSignal>();
            startSignal.Dispatch();
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            implicitBinder.ScanForAnnotatedClasses(new string[] { "Ghostpunch.OnlyDown" });
        }
    }
}