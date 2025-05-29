using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageToPlayer = 20f;

   void OnTriggerEnter2D(Collider2D collision) 
    {
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null && collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damageToPlayer);
        }
    }
}
