using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerPosition : MonoBehaviour
{
    public void SavePositionAndLoadScene(string sceneToLoad)
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        Vector3 pos = transform.position;
        PlayerPrefs.SetFloat(currentLevel + "_LastX", pos.x);
        PlayerPrefs.SetFloat(currentLevel + "_LastY", pos.y);
        PlayerPrefs.SetFloat(currentLevel + "_LastZ", pos.z);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneToLoad);
    }
}
