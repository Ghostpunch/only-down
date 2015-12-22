using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    [Serializable]
    public class PlayerModel
    {
        [SerializeField]
        private float _moveSpeed = 5f;
        [SerializeField]
        private float _fallSpeed = 5f;

        public float MoveSpeed { get { return _moveSpeed; } }
        public float FallSpeed { get { return _fallSpeed; } }
    }
}
