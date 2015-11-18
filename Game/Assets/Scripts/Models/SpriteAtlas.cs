using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Zenject;

namespace Ghostpunch.OnlyDown
{
    [Serializable]
    public class SpriteAtlas
    {
        [SerializeField]
        private Texture2D _atlasTexture = null;
        [SerializeField]
        private TextAsset _atlasData = null;
        [SerializeField]
        private int _pixelsPerUnit = 256;

        private Dictionary<string, Sprite> _spritesheet = new Dictionary<string, Sprite>();

        private void Import(Texture2D texture, TextAsset atlasData)
        {
            if (texture == null || atlasData == null)
                throw new ArgumentNullException("In order to properly generate a sprite atlas, a texture and atlas data must be provided.");

            var json = (MiniJSON.Json.Deserialize(atlasData.text) as Dictionary<string, object>)["frames"] as Dictionary<string, object>;
            var imageHeight = texture.height;

            foreach (var entry in json)
            {
                var imageName = entry.Key;
                var properties = entry.Value as Dictionary<string, object>;
                var spriteFrame = properties["frame"] as Dictionary<string, object>;
                var spritePivot = properties["pivot"] as Dictionary<string, object>;

                var pivotPoint = new Vector2(
                    Convert.ToSingle(spritePivot["x"]),
                    Convert.ToSingle(spritePivot["y"]));

                var spriteY = Convert.ToSingle(spriteFrame["y"]);
                var spriteHeight = Convert.ToSingle(spriteFrame["h"]);
                var spriteRect = new Rect(
                    Convert.ToSingle(spriteFrame["x"]),
                    imageHeight - spriteY - spriteHeight,
                    Convert.ToSingle(spriteFrame["w"]),
                    spriteHeight);

                var newSprite = Sprite.Create(texture, spriteRect, pivotPoint, _pixelsPerUnit);
                newSprite.name = imageName;

                _spritesheet.Add(imageName, newSprite);
            }
        }

        public bool Contains(string spriteName)
        {
            if (_spritesheet.Count == 0 && _atlasTexture != null && _atlasData != null)
                Import(_atlasTexture, _atlasData);

            if (_spritesheet.Count > 0 && !String.IsNullOrEmpty(spriteName))
                return _spritesheet.ContainsKey(spriteName);

            return false;
        }

        public Sprite GetSprite(string spriteName)
        {
            Sprite cachedSprite = null;

            if (Contains(spriteName))
                cachedSprite = _spritesheet[spriteName];

            return cachedSprite;
        }
    }
}
