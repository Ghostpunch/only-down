using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] _scenes = null;

    // Use this for initialization
    void Start()
    {
        foreach (var scene in _scenes)
        {
            Debug.Log("Loading Scene: " + scene);
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }

        Destroy(gameObject);
    }
}
