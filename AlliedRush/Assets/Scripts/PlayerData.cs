using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public Vector3 LastPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LastPosition = Vector3.zero;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


