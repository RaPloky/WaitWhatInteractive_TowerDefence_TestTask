using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    public static int Money;
    public static PlayerStats instance;
    public int startMoney = 150;

    private void Start()
    {
        Money = startMoney;
        moneyText.text = "$" + startMoney.ToString();
        instance = this;
    }
    public void UpdateMoneyText()
    {
        moneyText.text = "$" + Money.ToString();
    }
}
