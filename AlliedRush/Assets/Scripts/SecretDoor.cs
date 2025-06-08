using UnityEngine;

public class SecretDoor : MonoBehaviour
{
    [SerializeField] private string puzzleKey = "Puzzle1Solved";
    [SerializeField] private bool resetKeyOnStart = false; // Set true for testing only

    void Start()
    {
        if (resetKeyOnStart)
        {
            PlayerPrefs.DeleteKey(puzzleKey);
            Debug.Log($"[SecretDoor] Reset key '{puzzleKey}'");
        }

        if (PlayerPrefs.GetInt(puzzleKey, 0) == 1)
        {
            Debug.Log($"[SecretDoor] Key '{puzzleKey}' found. Door will open.");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"[SecretDoor] Key '{puzzleKey}' missing or 0. Door stays closed.");
        }
    }
}




