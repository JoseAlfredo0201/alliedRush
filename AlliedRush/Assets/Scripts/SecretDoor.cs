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
        }

        if (PlayerPrefs.GetInt(puzzleKey, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }
}




