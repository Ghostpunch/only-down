using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        Application.LoadLevelAsync(level);
    }

    public void LoadLevel(string level)
    {
        Application.LoadLevelAsync(level);
    }
}
