using Ghostpunch.OnlyDown.Common.ViewModels;
using Ghostpunch.OnlyDown.Menu.Views;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Menu.ViewModels
{
    public class MainMenuViewModel : ViewModelBase<MainMenuView>
    {
        public override void OnRegister()
        {
            base.OnRegister();

            View._startSignal.AddListener(OnStartGame);
        }

        private void OnStartGame()
        {
            Application.LoadLevel(1);
        }
    }
}
