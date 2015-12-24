using Ghostpunch.OnlyDown.Command;
using Ghostpunch.OnlyDown.Messaging;

namespace Ghostpunch.OnlyDown
{
    public class MenuViewModel : ObservableObject
    {
        private RelayCommand _onStartGame = null;
        public RelayCommand OnStartGame
        {
            get
            {
                return _onStartGame ?? (_onStartGame = new RelayCommand(() =>
                {
                    MessageSystem.Default.Broadcast(new GameStartMessage());
                    gameObject.SetActive(false);
                }));
            }
        }

        private RelayCommand _onRestartGame = null;
        public RelayCommand OnRestartGame
        {
            get
            {
                return _onRestartGame ?? (_onRestartGame = new RelayCommand(() =>
                {
                    MessageSystem.Default.Broadcast(new GameRestartMessage());
                    gameObject.SetActive(false);
                }));
            }
        }
    }
}
