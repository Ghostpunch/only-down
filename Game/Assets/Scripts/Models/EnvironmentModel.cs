using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    [Serializable]
    public class EnvironmentModel
    {
        [SerializeField]
        private int _sandVariations = 1, _cellsPerVariation = 15;
        public int SandVariations { get { return _sandVariations; } }
        public int CellsPerVariation { get { return _cellsPerVariation; } }

        [SerializeField]
        private SpriteAtlas[] _spriteAtlases = null;
        public SpriteAtlas[] SpriteAtlases { get { return _spriteAtlases; } }
    }
}
