using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class WallDisappears : MonoBehaviour
{
    public float delayInSeconds = 120f; // 2 minutes
    public AudioClip disappearSound;    // Assign your sound in the Inspector

    private Tilemap tilemap;
    private AudioSource audioSource;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // Create an AudioSource if not already on the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;

        Invoke("ClearTilemap", delayInSeconds);
    }

    void ClearTilemap()
    {
        if (disappearSound != null)
        {
            audioSource.PlayOneShot(disappearSound);
        }
        else
        {
            Debug.LogWarning("No disappear sound assigned.");
        }

        tilemap.ClearAllTiles();
        Debug.Log("Walls have disappeared.");
    }
}


