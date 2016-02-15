//using Ghostpunch.OnlyDown.Common;
//using Ghostpunch.OnlyDown.Common.Commands;
//using Ghostpunch.OnlyDown.Common.ViewModels;
//using Ghostpunch.OnlyDown.Menu.Views;
//using UnityEngine;

//namespace Ghostpunch.OnlyDown.Menu.ViewModels
//{
//    public class GameOverViewModel : ViewModelBase
//    {
//        [Inject]
//        public GameStartSignal GameStart { get; set; }

//        private RelayCommand _restartButtonPressedCommand;
//        public RelayCommand RestartButtonPressedCommand
//        {
//            get
//            {
//                return _restartButtonPressedCommand ?? (_restartButtonPressedCommand = new RelayCommand(() =>
//                {
//                    GameStart.Dispatch();
//                }));
//            }
//        }
//    }
//}
