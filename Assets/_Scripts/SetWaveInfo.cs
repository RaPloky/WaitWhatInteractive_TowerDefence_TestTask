using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetWaveInfo : MonoBehaviour
{
    private static TextMeshProUGUI _waveInfo;

    private void Awake()
    {
        _waveInfo = GetComponent<TextMeshProUGUI>();
    }
    public static void UpdateWaveInfo(int currentWaveIndex, int levelWavesCount)
    {
        _waveInfo.text = $"Wave: {currentWaveIndex}/{levelWavesCount}";
    }
}
