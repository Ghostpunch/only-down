//using Ghostpunch.OnlyDown.Common;
//using Ghostpunch.OnlyDown.Common.Commands;
//using Ghostpunch.OnlyDown.Menu.ViewModels;
//using strange.extensions.context.api;
//using strange.extensions.context.impl;
//using UnityEngine;

//namespace Ghostpunch.OnlyDown.Main
//{
//    public class MainContext : SignalContext
//    {
//        public MainContext(MonoBehaviour view) : base(view) { }
//        public MainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags) { }

//        protected override void mapBindings()
//        {
//            base.mapBindings();

//            #region Injections

//            if (Context.firstContext == this)
//            {
//                injectionBinder.Bind<GameStartSignal>().ToSingleton().CrossContext();
//                injectionBinder.Bind<GameOverSignal>().ToSingleton().CrossContext();
//                injectionBinder.Bind<ShutdownSignal>().ToSingleton().CrossContext();

//                injectionBinder.Bind<MenuViewModel>().CrossContext();
//                injectionBinder.Bind<MainMenuViewModel>().CrossContext();
//                injectionBinder.Bind<GameOverViewModel>().CrossContext();
//            }

//            #endregion

//            #region Commands

//            commandBinder.Bind<StartSignal>().To<MainStartupCommand>();
//            commandBinder.Bind<ShutdownSignal>().To<QuitApplicationCommand>();

//            #endregion
//        }
//    }
//}
