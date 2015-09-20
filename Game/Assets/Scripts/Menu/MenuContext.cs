using System;
using Ghostpunch.OnlyDown.Common;
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

            #region Injection

            if (Context.firstContext == this)
            {
                injectionBinder.Bind<GameStartSignal>().ToSingleton();
                injectionBinder.Bind<GameOverSignal>().ToSingleton();
                injectionBinder.Bind<GameInputSignal>().ToSingleton();
                injectionBinder.Bind<LevelStartSignal>().ToSingleton();
                injectionBinder.Bind<LevelEndSignal>().ToSingleton();
                injectionBinder.Bind<UpdateLevelSignal>().ToSingleton();
                injectionBinder.Bind<UpdateLivesSignal>().ToSingleton();
                injectionBinder.Bind<UpdateScoreSignal>().ToSingleton();
                injectionBinder.Bind<ShutdownSignal>().ToSingleton();
            }

            #region ViewModels
            injectionBinder.Bind<MenuViewModel>().ToValue(new MenuViewModel());
            injectionBinder.Bind<MainMenuViewModel>().ToValue(new MainMenuViewModel());
            injectionBinder.Bind<GameOverViewModel>().ToValue(new GameOverViewModel());
            #endregion

            #endregion

            commandBinder.Bind<StartSignal>().To<MenuStartCommand>();
        }

        protected override void postBindings()
        {
            // Identify and bind the UI camera
            var camera = (contextView as GameObject).GetComponentInChildren<Camera>();
            if (camera == null)
                throw new Exception("Couldn't find the UI Camera");

            injectionBinder.Bind<Camera>().ToValue(camera).ToName("Camera");

            if (Context.firstContext != this)
            {
                // Disable the AudioListener
                var listener = (contextView as GameObject).GetComponentInChildren<AudioListener>();
                if (listener != null)
                    listener.enabled = false;

                // Disable the directional light
                var lights = (contextView as GameObject).GetComponentsInChildren<Light>();
                foreach (var light in lights)
                    light.gameObject.SetActive(false);
            }

            base.postBindings();
        }
    }
}