using UnityEngine;
using System.Collections;
using Zenject;
using System;

namespace Ghostpunch.OnlyDown
{
    public class MenuInstaller : MonoInstaller
    {
        public GameObject _mainMenu = null;
        public GameObject _gameOver = null;

        public override void InstallBindings()
        {
            if (_mainMenu != null)
            {
                var menuUIView = _mainMenu.GetComponent<MenuView>();
                Container.Bind<MenuView>().ToSingleInstance(menuUIView).WhenInjectedInto<MainMenuPresenter>();

                Container.Bind<IInitializable>().ToSingle<MainMenuPresenter>();
                Container.Bind<MainMenuPresenter>().ToSingle();
            }

            if (_gameOver != null)
            {
                var gameOverView = _gameOver.GetComponent<MenuView>();
                Container.Bind<MenuView>().ToSingleInstance(gameOverView).WhenInjectedInto<GameOverPresenter>();

                Container.Bind<IInitializable>().ToSingle<GameOverPresenter>();
                Container.Bind<GameOverPresenter>().ToSingle();
            }
        }
    }
}
