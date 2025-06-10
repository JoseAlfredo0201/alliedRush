using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleComputer : MonoBehaviour, IInteractable
{
    [SerializeField] private string puzzleSceneName = "Puzzle_Level1";

    public void OnAction(GameObject caller)
    {
        if (caller != null)
        {
            Vector3 pos = caller.transform.position;
            string sceneName = SceneManager.GetActiveScene().name;

            PlayerPrefs.SetFloat(sceneName + "_x", pos.x);
            PlayerPrefs.SetFloat(sceneName + "_y", pos.y);
            PlayerPrefs.SetFloat(sceneName + "_z", pos.z);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(puzzleSceneName);
    }
}





