using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OnlyDown.Environment
{
    public class EnvironmentController
    {
        private Settings _settings = null;

        public EnvironmentController(Settings settings)
        {
            _settings = settings;
        }

        [Serializable]
        public class Settings
        {
            public float _cellSize;

            public GameObject[] _leftWallPiecePrefabs;
            public GameObject[] _rightWallPiecePrefabs;
        }
    }
}
