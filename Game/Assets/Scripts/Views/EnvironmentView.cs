using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Ghostpunch.OnlyDown
{
    public class EnvironmentView : MonoBehaviour
    {
        [SerializeField]
        private ViewSettings _settings = null;
        public ViewSettings Settings { get { return _settings; } }

        [Serializable]
        public class ViewSettings
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
}
