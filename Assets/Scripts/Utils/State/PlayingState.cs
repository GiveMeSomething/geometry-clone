using UnityEngine;
using System.Collections;

public class PlayingState : GameState
{
    private int score;
    private float gameStartTime;

    public PlayingState(GameManager gameManager) : base(gameManager)
    {
        score = 0;
        gameStartTime = Time.time;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Playing State");
    }

    public override void Update()
    {
        //updateTime
        float gameTime = Time.time - gameStartTime;
        score = ScoreManager.CalculateScore(gameManager.playerBehaviours.totalDistanceTraveled, gameTime);
        // Update the player's score based on some game logic
        gameManager.UpdateScore(score);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Playing State");
    }
}


