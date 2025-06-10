using UnityEngine;
using UnityEngine.SceneManagement;

public class RestorePlayerPosition : MonoBehaviour
{
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Only restore if a saved key exists
        if (PlayerPrefs.HasKey(sceneName + "_x"))
        {
            float x = PlayerPrefs.GetFloat(sceneName + "_x");
            float y = PlayerPrefs.GetFloat(sceneName + "_y");
            float z = PlayerPrefs.GetFloat(sceneName + "_z");

            transform.position = new Vector3(x, y, z);
        }
    }
}


