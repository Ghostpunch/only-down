//using Ghostpunch.OnlyDown.Common;
//using Ghostpunch.OnlyDown.Common.Commands;
//using Ghostpunch.OnlyDown.Common.ViewModels;
//using Ghostpunch.OnlyDown.Menu.Views;
//using UnityEngine;

//namespace Ghostpunch.OnlyDown.Menu.ViewModels
//{
//    public class MainMenuViewModel : ViewModelBase
//    {
//        [Inject]
//        public GameStartSignal GameStart { get; set; }

//        [Inject]
//        public ShutdownSignal Shutdown { get; set; }

//        private RelayCommand _startButtonPressedCommand;
//        public RelayCommand StartButtonPressedCommand
//        {
//            get
//            {
//                return _startButtonPressedCommand ?? (_startButtonPressedCommand = new RelayCommand(() =>
//                {
//                    GameStart.Dispatch();
//                }));
//            }
//        }

//        private RelayCommand _quitButtonPressedCommand;
//        public RelayCommand QuitButtonPressedCommand
//        {
//            get
//            {
//                return _quitButtonPressedCommand ?? (_quitButtonPressedCommand = new RelayCommand(() =>
//                {
//                    Shutdown.Dispatch();
//                }));
//            }
//        }
//    }
//}
