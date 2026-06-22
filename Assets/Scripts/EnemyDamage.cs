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
                GetComponent<Animator>().SetTrigger("Attack");
            }
            else
            {
                playerMovement.knockRight = false;
                GetComponent<Animator>().SetTrigger("Attack");
            }
            playerHealth.TakeDamage(damage);
            if (playerHealth.health <= 0)
            {
                GetComponent<EnemyMovement>().playerDead = true;
            }
        }
    }
    void Awake()
{
    int enemyLayer = LayerMask.NameToLayer("Enemies");
    Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);
}
}
