using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public int health = 1;
    private int correctLayer;

    void Start()
    {
        correctLayer = gameObject.layer;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (gameObject.tag == "Enemy")
            {
                EnemyNumber enemyNumber = gameObject.GetComponent<EnemyNumber>();
                EquationManager equationManager = FindObjectOfType<EquationManager>();

                if (enemyNumber && equationManager && equationManager.CheckAnswer(enemyNumber.number))
                {
                    // Correct enemy was hit, destroy the enemy.
                    Destroy(gameObject);
                }
                else
                {
                    // Wrong enemy was hit, damage the player.
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player)
                    {
                        DamageHandler playerDamageHandler = player.GetComponent<DamageHandler>();
                        if (playerDamageHandler)
                        {
                            playerDamageHandler.TakeDamage();
                        }
                    }
                }

                // Destroy the bullet after it hits something.
                Destroy(col.gameObject);
            }
        }
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
