using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Object[] _scenes = null;

    // Use this for initialization
    void Start()
    {
        foreach (var scene in _scenes)
        {
            Debug.Log("Loading Scene: " + scene.name);
            SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
        }

        Destroy(gameObject);
    }
}
