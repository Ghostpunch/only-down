using UnityEngine;
using System.Collections;
using Zenject;
using System;

namespace Ghostpunch.OnlyDown
{
    public class GameOverPresenter : IInitializable
    {
        private MenuView _view = null;

        public GameOverPresenter(MenuView view)
        {
            _view = view;
        }

        public void Initialize()
        {
        }
    }
}
