using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public float attackDuration = 0.3f;
    public int enemiesDefeated = 0;
    private bool isAttacking;
    private float attackTimer;
    private readonly HashSet<Collider2D> hitEnemiesThisAttack = new HashSet<Collider2D>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartAttack();
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
            }
            else
            {
                PerformAttackHitDetection();
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        attackTimer = attackDuration;
        hitEnemiesThisAttack.Clear();
        animator.SetTrigger("Attack1");
        PerformAttackHitDetection();
    }

    void PerformAttackHitDetection()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (hitEnemiesThisAttack.Add(enemy))
            {
                enemy.GetComponent<Animator>().SetTrigger("Death");
                enemy.enabled = false;
                enemiesDefeated++;
            }
        }
    }
}
