using UnityEngine;
using System.Collections;
using Zenject;
using System;
using OnlyDown.Messaging;
using OnlyDown.Environment;

namespace OnlyDown
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings _sceneSettings = null;

        public override void InstallBindings()
        {
            Container.Bind<IMessageSystem>().ToSingle<MessageSystem>();
            Container.Bind<Camera>(Cameras.Main.ToString()).ToSingleInstance(_sceneSettings.MainCamera);

            MapEnvironmentBindings();
            MapPlayerBindings();
        }

        private void MapEnvironmentBindings()
        {
            Container.Bind<EnvironmentController>().ToSingle();

            Container.Bind<EnvironmentController.Settings>().ToSingleInstance(_sceneSettings.Environment.Settings);
        }

        private void MapPlayerBindings()
        {
            Container.Bind<PlayerView>().ToTransientPrefab<PlayerView>(_sceneSettings.Player.Prefab).WhenInjectedInto<PlayerController>();

            Container.Bind<IFixedTickable>().ToSingle<PlayerController>();
            Container.Bind<IInitializable>().ToSingle<PlayerController>();
            Container.Bind<PlayerController>().ToSingle();

            Container.Bind<PlayerController.Settings>().ToSingleInstance(_sceneSettings.Player.Settings);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            private Camera _mainCamera = null;
            [SerializeField]
            private PlayerSettings _player = null;
            [SerializeField]
            private EnvironmentSettings _environment = null;

            public Camera MainCamera { get { return _mainCamera; } }
            public PlayerSettings Player { get { return _player; } }
            public EnvironmentSettings Environment { get { return _environment; } }

            [Serializable]
            public class PlayerSettings
            {
                [SerializeField]
                private GameObject _prefab = null;
                [SerializeField]
                private PlayerController.Settings _settings = null;

                public GameObject Prefab { get { return _prefab; } }
                public PlayerController.Settings Settings { get { return _settings; } }
            }

            [Serializable]
            public class EnvironmentSettings
            {
                [SerializeField]
                private EnvironmentController.Settings _settings = null;
                public EnvironmentController.Settings Settings { get { return _settings; } }
            }
        }
    }
}
