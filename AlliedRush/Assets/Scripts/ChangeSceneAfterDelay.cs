using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAfterDelay : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 1;
    [SerializeField] private float delayInSeconds = 3f;

    private void Start()
    {
        Invoke(nameof(LoadScene), delayInSeconds);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

