using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI wavesInfo;
    [SerializeField] int startMoney = 150;
    [SerializeField] int startLives = 3;
    public int livesLimit = 5;

    public static int Money;
    public static int AmountOfLives;
    public static int WavesSurvived;
    // Singleton
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one manager!");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        Money = startMoney;
        AmountOfLives = startLives;
        WavesSurvived = 0;
        moneyText.text = "$" + startMoney.ToString();
        livesText.text = startLives + "/" + livesLimit;
    }
    public void UpdateMoneyText()
    {
        moneyText.text = "$" + Money.ToString();
    }
    public void UpdateLivesAmount()
    {
        livesText.text = AmountOfLives + "/" + livesLimit;
    }
    public void UpdateWaveInfo(int currentWaveIndex, int levelWavesCount)
    {
        wavesInfo.text = currentWaveIndex + "/" + levelWavesCount;
    }
}
