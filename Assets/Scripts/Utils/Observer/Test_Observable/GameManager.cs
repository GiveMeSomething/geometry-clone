using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public PlayerBehaviours playerBehaviours;
    private GameState currentState;

    private float gameStartTime;
    private float gameEndTime;
    //score
    private int score;

    private void Start()
    {
        currentState = new PlayingState(this);
        currentState.EnterState();
        playerBehaviours = FindObjectOfType<PlayerBehaviours>();
        if (playerBehaviours != null)
        {
            playerBehaviours.onGameOver.Subscribe(OnObstacleHit);
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.onReplayButtonClicked.Subscribe(OnReplayButtonClicked);
        }
        gameStartTime = Time.time;
    }

    private void OnReplayButtonClicked(bool repeat)
    {
        if (repeat)
        {
            //reset game
            Time.timeScale = 1;
            //change state
            currentState = new PlayingState(this);
            currentState.EnterState();
            //reset score
            score = 0;
            //reset time
            gameStartTime = Time.time;
            //hide game over screen
            gameOverScreen.HideGameOverScreen();
        }
    }

    private void Update()
    {
        currentState.Update();
    }

    private void OnObstacleHit(bool hit)
    {
        if (hit)
        {
            gameEndTime = Time.time;
            //pause game
            Time.timeScale = 0;
            //change state
            currentState = new GameOverState(this);
            currentState.EnterState();
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

