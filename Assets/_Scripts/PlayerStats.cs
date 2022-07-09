using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] int startMoney = 150;
    [SerializeField] int startLives = 3;
    public int livesLimit = 5;

    public static int Money;
    public static int AmountOfLives;
    public static PlayerStats instance;

    private void Start()
    {
        Money = startMoney;
        AmountOfLives = startLives;
        moneyText.text = "$" + startMoney.ToString();
        livesText.text = startLives + " / " + livesLimit + " LIVES";
        instance = this;
    }
    public void UpdateMoneyText()
    {
        moneyText.text = "$" + Money.ToString();
    }
    public void UpdateLivesAmount()
    {
        livesText.text = AmountOfLives + " / " + livesLimit + " LIVES";
    }
}
