using UnityEngine;
using System.Collections;

public static class ScoreManager
{
    public static int CalculateScore(float distanceTraveled, float gameTime)
    {
        // Calculate the score based on the player's distance traveled and game time
        // and return the result
        //convert to int
        int score = Mathf.RoundToInt(distanceTraveled / gameTime);
        return score;
    }
}

