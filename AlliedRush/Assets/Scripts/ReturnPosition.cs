using UnityEngine;

public class ReturnPosition : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        if (PlayerData.Instance != null && player != null)
        {
            player.transform.position = PlayerData.Instance.LastPosition;
        }
    }
}


