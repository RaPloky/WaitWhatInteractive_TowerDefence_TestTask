using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static bool IsGameEnded;
    [SerializeField] GameObject gameOverUI;

    private void Awake()
    {
        IsGameEnded = false;
    }
    private void Update()
    {
        if (IsGameEnded)
            return;

        if (Mathf.Approximately(PlayerStats.AmountOfLives, 0))
            EndGame();
    }
    private void EndGame()
    {
        gameOverUI.SetActive(true);
        IsGameEnded = true;
    }
}
