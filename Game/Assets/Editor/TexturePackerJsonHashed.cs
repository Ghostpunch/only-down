using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Editor
{
    public class TexturePackerJsonHashed : EditorWindow
    {
        private Object _textObject;
        private Texture2D _textureObject;
        private SpriteAlignment _pivot = SpriteAlignment.Center;
        private Vector2 _customPivot = new Vector2(0.5f, 0.5f);

        [MenuItem("Window/TexturePacker/Import")]
        public static void OpenWindow()
        {
            EditorWindow.GetWindow<TexturePackerJsonHashed>(false, "TexturePacker Import");
        }

        void OnGUI()
        {
            _textObject = EditorGUILayout.ObjectField("File: ", _textObject, typeof(TextAsset), false);
            _textureObject = (Texture2D)EditorGUILayout.ObjectField("Texture: ", _textureObject, typeof(Texture2D), false);
            _pivot = (SpriteAlignment)EditorGUILayout.EnumPopup("Pivot: ", _pivot);
            if (_pivot == SpriteAlignment.Custom)
            {
                Vector2 customPivot = EditorGUILayout.Vector2Field("Custom pivot: ", _customPivot);

                if (customPivot != _customPivot)
                {
                    _customPivot.x = Mathf.Clamp01(customPivot.x);
                    _customPivot.y = Mathf.Clamp01(customPivot.y);
                }
            }

            if (GUILayout.Button("Import") && _textObject != null && _textureObject != null)
            {
                Import();
            }
        }

        private void Import()
        {
            var path = AssetDatabase.GetAssetPath(_textObject);
            var jsonText = File.ReadAllText(path);

            var json = (MiniJSON.Json.Deserialize(jsonText) as Dictionary<string, object>)["frames"] as Dictionary<string, object>;
            var spriteList = new List<SpriteMetaData>();
            var imageHeight = _textureObject.height;

            foreach (var entry in json)
            {
                var imageName = entry.Key;
                var properties = entry.Value as Dictionary<string, object>;
                var spriteFrame = properties["frame"] as Dictionary<string, object>;
                var spritePivot = properties["pivot"] as Dictionary<string, object>;

                // Parse the pivot point
                var customPivot = new Vector2(
                    System.Convert.ToSingle(spritePivot["x"]), 
                    System.Convert.ToSingle(spritePivot["y"]));

                var spriteY = System.Convert.ToSingle(spriteFrame["y"]);
                var spriteHeight = System.Convert.ToSingle(spriteFrame["h"]);
                // create the sprite rect
                var spriteRect = new Rect(
                    System.Convert.ToSingle(spriteFrame["x"]),
                    imageHeight - spriteY - spriteHeight,
                    System.Convert.ToSingle(spriteFrame["w"]),
                    spriteHeight);

                var spriteData = new SpriteMetaData()
                {
                    name = imageName,
                    rect = spriteRect,
                    alignment = (int)SpriteAlignment.Custom,
                    pivot = customPivot
                };

                // add to list
                spriteList.Add(spriteData);
            }

            if (spriteList.Count > 0)
            {
                // Import texture
                TextureImporter importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(_textureObject)) as TextureImporter;

                // Add spritesheets
                importer.spritesheet = spriteList.ToArray();

                // Configure texture
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Multiple;

                // Import and force update
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(_textureObject), ImportAssetOptions.ForceUpdate);
            }
        }
    }
}
