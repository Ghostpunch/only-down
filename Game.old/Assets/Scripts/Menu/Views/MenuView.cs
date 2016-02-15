//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using Ghostpunch.OnlyDown.Common;
//using Ghostpunch.OnlyDown.Common.Views;
//using Ghostpunch.OnlyDown.Menu.ViewModels;
//using strange.extensions.mediation.impl;
//using strange.extensions.signal.impl;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Ghostpunch.OnlyDown.Menu.Views
//{
//    public class MenuView : View
//    {
//        public GameObject _mainMenu, _gameOverMenu;

//        [Inject]
//        public MenuViewModel VM { get; set; }

//        protected override void Start()
//        {
//            base.Start();

//            VM.PropertyChanged += VM_PropertyChanged;
//        }

//        private void VM_PropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            if (e.PropertyName == "IsMainMenuVisible")
//            {
//                _mainMenu.SetActive(VM.IsMainMenuVisible);
//            }
//            else if (e.PropertyName == "IsGameOverVisible")
//            {
//                //Debug.Log("Setting _gameOverMenu.SetActive to ")
//                _gameOverMenu.SetActive(VM.IsGameOverVisible);
//            }
//            if (e.PropertyName == "MainMenuAlpha")
//            {
//                _mainMenu.GetComponent<CanvasRenderer>().SetAlpha(VM.MainMenuAlpha);
//            }
//        }
//    }
//}
