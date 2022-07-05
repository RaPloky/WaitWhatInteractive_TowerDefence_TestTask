using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCoinsInfo : MonoBehaviour
{
    private static TextMeshProUGUI _coinsInfo;

    private void Awake()
    {
        _coinsInfo = GetComponent<TextMeshProUGUI>();
    }
    public static void UpdateCoinsInfo(int currentCoins)
    {
        _coinsInfo.text = $"{currentCoins}";
    }
}
