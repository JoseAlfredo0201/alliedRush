using UnityEngine;

public class LongRangeShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    public AudioClip shootSound; // 🎵 Sonido de disparo
    private AudioSource audioSource;

    private float timer;
    private GameObject player;
    public float shootCooldown = 3f; // 🕒 Cooldown en segundos

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>(); // 👂 Obtener componente de sonido
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 20)
        {
            timer += Time.deltaTime;

            if (timer > shootCooldown)
            {
                timer = 0;
                shoot();
            }
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound); // 🔊 Reproduce el sonido
        }
    }
}

