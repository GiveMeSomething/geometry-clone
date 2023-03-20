using UnityEngine;
using System.Collections;

public class GameOverState : GameState
{
    public GameOverState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entering Game Over State");

        // Show the game over screen and pass the player's final score
        gameManager.gameOverScreen.ShowGameOverScreen(gameManager.GetScore());
        // Save the player's score to PlayerPrefs or wherever you want to store it
        PlayerPrefs.SetFloat("PlayerScore", gameManager.GetScore());
    }

    public override void Update()
    {
        // Nothing to update in this state
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Game Over State");
    }
}
