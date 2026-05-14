using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement.knockbackDuration = playerMovement.knockbackTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.knockRight = true;
            }
            else
            {
                playerMovement.knockRight = false;
            }
            playerHealth.TakeDamage(damage);
        }
    }
}
