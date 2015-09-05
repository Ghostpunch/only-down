using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.context.api;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using Ghostpunch.OnlyDown.Menu.Signals;
using Ghostpunch.OnlyDown.Menu.Commands;

namespace Ghostpunch.OnlyDown.Menu
{
    public class MenuContext : MVCSContext
    {
        public MenuContext(MonoBehaviour view) : base(view)
        {
        }

        public MenuContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();

            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        public override IContext Start()
        {
            base.Start();

            var startSignal = injectionBinder.GetInstance<StartSignal>();
            startSignal.Dispatch();

            return this;
        }

        protected override void mapBindings()
        {
            commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
        }
    }
}