using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Object[] _scenes = null;

    // Use this for initialization
    void Start()
    {
        foreach (var scene in _scenes)
        {
            Debug.Log(scene.name);
            Application.LoadLevelAdditive(scene.name);
        }

        Destroy(gameObject);
    }
}
