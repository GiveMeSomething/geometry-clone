using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public JumpDetector jumpDetector;

    private void Start()
    {
        jumpDetector = FindObjectOfType<JumpDetector>();
        jumpDetector.onGameOver.Subscribe(OnObstacleHit);
    }

    private void OnObstacleHit(bool hit)
    {
        Time.timeScale = 0;
        gameOverScreen.ShowGameOverScreen();
    }
}

