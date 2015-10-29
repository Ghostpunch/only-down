using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Text;
using System;
using System.IO;

public class TagScriptGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Tag Script")]
    public static void GenerateTagScript()
    {
        var tags = InternalEditorUtility.tags;
        var builder = new StringBuilder();

        builder.AppendLine("public static class Tags\n{");
        foreach (var tag in tags)
        {
            builder.AppendLine(String.Format("public const string {0} = \"{1}\";", tag.Replace(' ', '_'), tag));
        }
        builder.AppendLine("}");

        var layers = InternalEditorUtility.layers;

        builder.AppendLine("public static class Layers\n{");
        foreach (var layer in layers)
        {
            builder.AppendLine(String.Format("public const string {0} = \"{1}\";", layer.Replace(' ', '_'), layer));
        }
        builder.AppendLine("}");

        var pathToAsset = Path.Combine(Application.dataPath, "Scripts/TagsAndLayers.cs");
        DeleteFile(Path.Combine(Application.dataPath, "Scripts/Tags.cs")); // This is just in case you previously generated the tags script.
        CreateFile(pathToAsset, builder.ToString());
    }

    private static void CreateFile(string path, string content)
    {
        var fileStream = File.CreateText(path);
        fileStream.Write(content);
        fileStream.Close();

        AssetDatabase.Refresh();
    }

    private static void DeleteFile(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
    }
}
