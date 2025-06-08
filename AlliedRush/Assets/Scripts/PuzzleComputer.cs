using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleComputer : MonoBehaviour, IInteractable
{
    [SerializeField] private string puzzleSceneName = "Puzzle_Level1";

    public void OnAction(GameObject caller)
    {
        // Save player last position before loading puzzle scene
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.LastPosition = caller.transform.position;
        }

        SceneManager.LoadScene(puzzleSceneName);
    }
}



