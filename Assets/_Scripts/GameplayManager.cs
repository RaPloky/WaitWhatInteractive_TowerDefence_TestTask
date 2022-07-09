using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static bool _isGameEnded = false;

    private void Update()
    {
        if (_isGameEnded)
            return;

        if (Mathf.Approximately(PlayerStats.AmountOfLives, 0))
            EndGame();
    }
    private void EndGame()
    {
        print("End game!");
        _isGameEnded = true;
    }
}
