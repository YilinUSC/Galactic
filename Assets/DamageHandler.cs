using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public int health = 1;
    public float invulnerablePeriod = 0;
    float invulnerableTimer = 0;
    private int correctLayer;

    void Start()
    {
        correctLayer = gameObject.layer;
    }

    void OnTriggerEnter2D()
    {
        Debug.Log("Trigger!");
        
        health--;
        invulnerableTimer = invulnerablePeriod;   //which means the play will be invulnerable in invulnerablePeriod of seconds
        gameObject.layer = 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        invulnerableTimer -= Time.deltaTime;
        if (invulnerableTimer <= 0)
        {
            gameObject.layer = correctLayer;
        }


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
