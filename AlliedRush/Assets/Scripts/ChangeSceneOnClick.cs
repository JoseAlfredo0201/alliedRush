using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 1;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

