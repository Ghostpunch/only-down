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
            if (_sceneSettings.Environment != null)
            {
                Container.BindInstance(_sceneSettings.Environment);
                Container.BindInstance(_sceneSettings.Environment.Settings);

                Container.Bind<IInitializable>().ToSingle<EnvironmentPresenter>();
                Container.Bind<EnvironmentPresenter>().ToSingle();
            }
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
            private EnvironmentView _environment = null;
            public EnvironmentView Environment { get { return _environment; } }

            [SerializeField]
            private GameObject _playerPrefab = null;
            public GameObject PlayerPrefab { get { return _playerPrefab; } }

            [SerializeField]
            private PlayerPresenter.Settings _playerSettings = null;
            public PlayerPresenter.Settings PlayerSettings { get { return _playerSettings; } }
        }
    }
}
