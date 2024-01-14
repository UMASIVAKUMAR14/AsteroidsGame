using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public MoveWithArrowKeys player;
    public TextMeshProUGUI scoreIndicator;
    public TextMeshProUGUI livesIndicator;
    public Transform gameOverPanel;
    public int lives = 3;
    public int score = 0;
    public bool gameOver = false;
    public float spawnDelay = 3.0f;

    private void Update()
    {
        livesIndicator.text = "Lives: " + lives.ToString();
        scoreIndicator.text = "Score: " + score.ToString();

        if (gameOver)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        //Debug.Log("Destroyed called");
        if (asteroid.size < 0.88f)
        {
            score += 100;
        }
        else if (asteroid.size < 1.21f)
        {
            score += 60;
        }
        else 
        {
            score += 40;
        }
    }
    
    public void PlayerDied()
    {
        lives-= 1;
        if (this.lives <= 0) 
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), spawnDelay);
        }

    }

    private void Respawn() 
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        gameOver = true;
        
    }


}
