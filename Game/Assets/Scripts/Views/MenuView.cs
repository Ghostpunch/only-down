namespace Ghostpunch.OnlyDown
{
    public class MenuView : BaseView<MenuViewModel>
    {
        public void OnStartGameButtonPressed()
        {
            ViewModel.OnStartGame.Execute(null);
        }

        public void OnRestartGameButtonPressed()
        {
            ViewModel.OnRestartGame.Execute(null);
        }
    }
}
