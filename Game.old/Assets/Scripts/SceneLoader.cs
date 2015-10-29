using UnityEngine;
using System.Collections;
using Zenject;
using System;

namespace OnlyDown
{
    public enum Cameras
    {
        Main,
    }

    public class SceneLoader : MonoBehaviour
    {
        public string[] sceneNames = null;

        void Start()
        {
            foreach (var scene in sceneNames)
                Application.LoadLevelAdditive(scene);

            Destroy(gameObject);
        }
    }
}
