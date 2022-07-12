using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static bool IsGameEnded;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameWonUI;

    private void Awake()
    {
        IsGameEnded = false;
    }
    private void Update()
    {
        if (IsGameEnded)
            return;

        if (Mathf.Approximately(PlayerStats.WavesSurvived, WaveSpawner.instance.levelWavesCount))
            WinGame();

        if (Mathf.Approximately(PlayerStats.AmountOfLives, 0))
            EndGame();
    }
    private void EndGame()
    {
        gameOverUI.SetActive(true);
        IsGameEnded = true;
    }
    private void WinGame()
    {
        gameWonUI.SetActive(true);
        IsGameEnded = true;
    }
}
