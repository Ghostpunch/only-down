using Ghostpunch.OnlyDown.Common;
using Ghostpunch.OnlyDown.Common.Signals;
using Ghostpunch.OnlyDown.Common.ViewModels;
using Ghostpunch.OnlyDown.Common.Views;
using Ghostpunch.OnlyDown.Menu.Commands;
using Ghostpunch.OnlyDown.Menu.ViewModels;
using Ghostpunch.OnlyDown.Menu.Views;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Menu
{
    public class MenuContext : SignalContext
    {
        public MenuContext(MonoBehaviour view) : base(view)
        {
        }

        public MenuContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            if (Context.firstContext == this)
            {
                injectionBinder.Bind<GameStartSignal>().ToSingleton();
                injectionBinder.Bind<GameEndSignal>().ToSingleton();
                injectionBinder.Bind<GameInputSignal>().ToSingleton();
                injectionBinder.Bind<LevelStartSignal>().ToSingleton();
                injectionBinder.Bind<LevelEndSignal>().ToSingleton();
                injectionBinder.Bind<UpdateLevelSignal>().ToSingleton();
                injectionBinder.Bind<UpdateLivesSignal>().ToSingleton();
                injectionBinder.Bind<UpdateScoreSignal>().ToSingleton();
            }

            commandBinder.Bind<StartSignal>().To<MenuStartCommand>();

            mediationBinder.Bind<MainMenuView>().To<MainMenuViewModel>();
            mediationBinder.Bind<ButtonView>().To<ButtonViewModel>();
        }
    }
}