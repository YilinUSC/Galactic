using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // If you're using TextMeshPro for the UI

public class DamageHandler : MonoBehaviour
{
    public int health = 1;
    public GameObject gameOverUI;  // Drag your game over UI panel here in the inspector
    private int correctLayer;
    
    
    public TextMeshProUGUI scoreText;  // Drag your score TextMeshProUGUI component here in the inspector
    int score = 0;  // Player's score
    
    public static DamageHandler playerInstance;
    

    void Start()
    {
        correctLayer = gameObject.layer;
        
        // Ensure the Game Over UI is hidden at start
        if (gameOverUI)
        {
            gameOverUI.SetActive(false);
        }
        
        UpdateScoreText();  // Initialize score text at the start
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
                    // Correct enemy was hit, destroy the enemy
                    Destroy(gameObject);
                    
                    // Increment the score and update the display
                    //score++;
                    //Debug.Log("Score incremented to: " + score);  //debug line
                    //UpdateScoreText();
                    
                    DamageHandler.playerInstance.IncrementScore();

                    
                    // Generate a new equation because the player answered correctly
                    equationManager.GenerateEquation();
                }
                else
                {
                    // Wrong enemy was hit, game over for the player
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

                // Destroy the bullet after it hits something
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
        if (gameObject.tag == "Player")
        {
            ShowGameOver();
        }
        Destroy(gameObject);
    }

    void ShowGameOver()
    {
        if (gameOverUI)
        {
            gameOverUI.SetActive(true);  // Show the Game Over UI
        }
        Time.timeScale = 0;  // Pause the game
    }
    
    void UpdateScoreText()
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + score;
        }
    }
    
    void Awake()
    {
        if (gameObject.tag == "Player")
        {
            if (playerInstance == null)
            {
                playerInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void IncrementScore()
    {
        score++;
        Debug.Log("Score incremented to: " + score);  //debug line
        UpdateScoreText();
    }
}
