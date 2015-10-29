using UnityEngine;
using System.Collections;
using Zenject;
using System;

namespace Ghostpunch.OnlyDown
{
    public enum Cameras
    {
        Main,
    }

    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings _sceneSettings = null;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().ToSingleInstance(_sceneSettings.MainCamera);

            MapEnvironmentBindings();
            MapPlayerBindings();
        }

        private void MapEnvironmentBindings()
        {
            Container.Bind<EnvironmentView>().ToTransientPrefab(_sceneSettings.EnvironmentPrefab).WhenInjectedInto<EnvironmentPresenter>();

            Container.Bind<IInitializable>().ToSingle<EnvironmentPresenter>();
            Container.Bind<EnvironmentPresenter>().ToSingle();

            Container.Bind<EnvironmentPresenter.Settings>().ToSingleInstance(_sceneSettings.EnvironmentSettings);
        }

        private void MapPlayerBindings()
        {
            Container.Bind<PlayerView>().ToTransientPrefab(_sceneSettings.PlayerPrefab).WhenInjectedInto<PlayerPresenter>();

            Container.Bind<IInitializable>().ToSingle<PlayerPresenter>();
            Container.Bind<IFixedTickable>().ToSingle<PlayerPresenter>();
            Container.Bind<PlayerPresenter>().ToSingle();

            Container.Bind<PlayerPresenter.Settings>().ToSingleInstance(_sceneSettings.PlayerSettings);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            private Camera _mainCamera = null;
            public Camera MainCamera { get { return _mainCamera; } }

            [SerializeField]
            private GameObject _environmentPrefab = null;
            public GameObject EnvironmentPrefab { get { return _environmentPrefab; } }

            [SerializeField]
            private GameObject _playerPrefab = null;
            public GameObject PlayerPrefab { get { return _playerPrefab; } }

            [SerializeField]
            private EnvironmentPresenter.Settings _environmentSettings = null;
            public EnvironmentPresenter.Settings EnvironmentSettings { get { return _environmentSettings; } }

            [SerializeField]
            private PlayerPresenter.Settings _playerSettings = null;
            public PlayerPresenter.Settings PlayerSettings { get { return _playerSettings; } }
        }
    }
}
