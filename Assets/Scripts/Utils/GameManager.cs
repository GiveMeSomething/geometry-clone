using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    [SerializeField]
    private PlayerBehaviour playerBehaviour;
    private int score;

    private float gameStartTime;
    private float gameEndTime;

    private bool isPlaying = false;

    private void Start()
    {
        playerBehaviour.GameOverEvent.Subscribe(OnObstacleHit);
        gameOverScreen.onReplayButtonClicked.Subscribe(OnReplayButtonClicked);
        gameStartTime = Time.time;
        isPlaying = true;
    }



    private void OnReplayButtonClicked(bool repeat)
    {
        if (repeat)
        {
             Debug.Log("onReplayButtonClicked called with value " + repeat);
        //reset score
        score = 0;
        //reset time
        gameStartTime = Time.time;
        //reset Map
        //change isPlaying state
        isPlaying = true;
        //hide game over screen
        gameOverScreen.HideGameOverScreen();
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            float gameTime = Time.time - gameStartTime;
            float distanceTraveled = playerBehaviour.totalSurviveTime;
            score = Mathf.RoundToInt(distanceTraveled / gameTime);
            UpdateScore(score);
        }
    }

    private void OnObstacleHit(bool hit)
    {
        if (hit)
        {
            gameEndTime = Time.time;
            //pause game
            Time.timeScale = 0;
            isPlaying = false;
            // Show the game over screen and pass the player's final score
            gameOverScreen.ShowGameOverScreen(score);
        }
    }

    public void UpdateScore(int score)
    {
        this.score = score;
    }

    //gets the score
    public float GetScore()
    {
        return score;
    }
}

