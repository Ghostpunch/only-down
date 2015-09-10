using UnityEngine;
using System.Collections;
using Ghostpunch.OnlyDown.Common;
using strange.extensions.context.api;
using Ghostpunch.OnlyDown.Common.Signals;
using strange.extensions.context.impl;
using Ghostpunch.OnlyDown.Game.Models;
using Ghostpunch.OnlyDown.Game.Commands;
using strange.extensions.pool.api;
using strange.extensions.pool.impl;
using Ghostpunch.OnlyDown.Game.Views;
using Ghostpunch.OnlyDown.Game.ViewModels;

namespace Ghostpunch.OnlyDown.Game
{
    public class GameContext : SignalContext
    {
        public GameContext(MonoBehaviour view) : base(view) { }

        public GameContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags) { }

        protected override void mapBindings()
        {
            base.mapBindings();

            #region Injections

            if (Context.firstContext == this)
            {
                injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();
            }

            injectionBinder.Bind<IGameConfig>().To<GameConfig>().ToSingleton();

            // Pools
            // Pools provide a recycling system that makes the game much more efficient.
            // Instead of destroying instances and reinstantiating them, which is expensive,
            // we "checkout" the instances from a pool, then return them when done.
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.SandPool);

            // Signals
            injectionBinder.Bind<GameStartedSignal>().ToSingleton();
            injectionBinder.Bind<LevelStartedSignal>().ToSingleton();

            injectionBinder.Bind<PlayerDigSignal>().ToSingleton();
            #endregion

            #region Commands

            if (Context.firstContext == this)
            {
                commandBinder.Bind<StartSignal>().To<GameModuleStartCommand>();
            }

            commandBinder.Bind<GameStartSignal>()
                .To<SetupLevelCommand>()
                .To<CleanupCommand>()
                .To<StartLevelCommand>()
                .InSequence();

            #endregion

            #region ViewModels

            mediationBinder.Bind<EnvironmentView>().To<EnvironmentViewModel>();
            mediationBinder.Bind<PlayerView>().To<PlayerViewModel>();

            #endregion
        }

        protected override void postBindings()
        {
            var sandPool = injectionBinder.GetInstance<IPool<GameObject>>(GameElement.SandPool);
            sandPool.instanceProvider = new ResourceInstanceProvider("sand", LayerMask.NameToLayer("Default"));
            sandPool.inflationType = PoolInflationType.INCREMENT;

            base.postBindings();
        }
    }
}