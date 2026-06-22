using UnityEngine;
using TMPro;

public class Counters : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text enemiesText;

    private PlayerHealth playerHealth;
    private PlayerAttack playerAttack;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null) playerHealth = FindAnyObjectByType<PlayerHealth>();

        playerAttack = GetComponent<PlayerAttack>();
        if (playerAttack == null) playerAttack = FindAnyObjectByType<PlayerAttack>();

        if (healthText == null)
        {
            var go = GameObject.Find("Health");
            if (go) healthText = go.GetComponent<TMP_Text>();
        }

        if (enemiesText == null)
        {
            var go = GameObject.Find("Enemies");
            if (go) enemiesText = go.GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        if (healthText != null && playerHealth != null)
            healthText.text = "Health: " + playerHealth.health;

        if (enemiesText != null && playerAttack != null)
            enemiesText.text = "Enemies defeated: " + playerAttack.enemiesDefeated;
    }
}
