using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    [Serializable]
    public class PowerUpModel
    {
        [SerializeField]
        private GameObject[] _powerupPrefabs = null;
        public GameObject[] PowerUpPrefabs { get { return _powerupPrefabs; } }
    }
}
