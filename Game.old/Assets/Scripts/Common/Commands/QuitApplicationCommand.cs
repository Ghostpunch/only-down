//using UnityEngine;
//using strange.extensions.command.impl;

//namespace Ghostpunch.OnlyDown.Common.Commands
//{
//    public class QuitApplicationCommand : Command
//    {
//        public override void Execute()
//        {
//            // If we are running in a standalone build of the game
//#if UNITY_STANDALONE
//            // Quit the application
//            Application.Quit();
//#endif

//            // If we are running in the editor
//#if UNITY_EDITOR
//            // Stop playing the scene
//            UnityEditor.EditorApplication.isPlaying = false;
//#endif
//        }
//    }
//}
