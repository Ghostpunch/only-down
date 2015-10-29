//using UnityEngine;
//using System.Collections;
//using Ghostpunch.OnlyDown.Common;
//using strange.extensions.context.api;
//using strange.extensions.context.impl;
//using Ghostpunch.OnlyDown.Game.Models;
//using Ghostpunch.OnlyDown.Game.Commands;
//using strange.extensions.pool.api;
//using strange.extensions.pool.impl;
//using Ghostpunch.OnlyDown.Game.Views;
//using Ghostpunch.OnlyDown.Game.ViewModels;

//namespace Ghostpunch.OnlyDown.Game
//{
//    public class GameContext : SignalContext
//    {
//        public GameContext(MonoBehaviour view) : base(view) { }

//        public GameContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags) { }

//        protected override void mapBindings()
//        {
//            base.mapBindings();

//            #region Injections

//            if (Context.firstContext == this)
//            {
//                injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();

//                injectionBinder.Bind<GameStartSignal>().ToSingleton();
//                injectionBinder.Bind<GameOverSignal>().ToSingleton();
//                injectionBinder.Bind<ShutdownSignal>().ToSingleton();
//            }

//            injectionBinder.Bind<IGameConfig>().To<GameConfig>().ToSingleton();

//            // Pools
//            // Pools provide a recycling system that makes the game much more efficient.
//            // Instead of destroying instances and reinstantiating them, which is expensive,
//            // we "checkout" the instances from a pool, then return them when done.
//            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.SandPool);
//            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.ItemPool);
//            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.EnemyPool);

//            // Signals
//            injectionBinder.Bind<GameStartedSignal>().ToSingleton();
//            injectionBinder.Bind<LevelStartedSignal>().ToSingleton();

//            injectionBinder.Bind<PlayerDigSignal>().ToSingleton();
//            injectionBinder.Bind<LevelScrollSignal>().ToSingleton();
//            #endregion

//            #region Commands

//            commandBinder.Bind<StartSignal>().To<GameModuleStartCommand>();
//            commandBinder.Bind<EnemyPlayerCollisionSignal>().To<EnemyPlayerCollisionCommand>();

//            commandBinder.Bind<GameStartSignal>()
//                .To<CleanupCommand>()
//                .To<SetupLevelCommand>()
//                .To<StartLevelCommand>()
//                .InSequence();

//            #endregion

//            #region ViewModels

//            mediationBinder.Bind<EnvironmentView>().To<EnvironmentViewModel>();
//            mediationBinder.Bind<PlayerView>().To<PlayerViewModel>();
//            mediationBinder.Bind<ScrollView>().To<ScrollViewModel>();
//            mediationBinder.Bind<ItemView>().To<ItemViewModel>();
//            mediationBinder.Bind<EnemyView>().To<EnemyViewModel>();

//            #endregion
//        }

//        protected override void postBindings()
//        {
//            var sandPool = injectionBinder.GetInstance<IPool<GameObject>>(GameElement.SandPool);
//            sandPool.instanceProvider = new ResourceInstanceProvider("sand", LayerMask.NameToLayer(Layers.Default));
//            sandPool.inflationType = PoolInflationType.INCREMENT;

//            var itemPool = injectionBinder.GetInstance<IPool<GameObject>>(GameElement.ItemPool);
//            itemPool.instanceProvider = new ResourceInstanceProvider("item", LayerMask.NameToLayer(Layers.Default));
//            itemPool.inflationType = PoolInflationType.INCREMENT;

//            var enemyPool = injectionBinder.GetInstance<IPool<GameObject>>(GameElement.EnemyPool);
//            enemyPool.instanceProvider = new ResourceInstanceProvider("enemy", LayerMask.NameToLayer(Layers.Default));
//            enemyPool.inflationType = PoolInflationType.INCREMENT;

//            base.postBindings();
//        }
//    }
//}