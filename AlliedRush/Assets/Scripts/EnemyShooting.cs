using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public AudioClip shootSound;
    private AudioSource audioSource;


    private float timer;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 10)
        {
            timer += Time.deltaTime;

            if (timer > 2)
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
