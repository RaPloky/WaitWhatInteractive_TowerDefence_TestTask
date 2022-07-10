using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetWaveInfo : MonoBehaviour
{
    private static TextMeshProUGUI _wavesInfo;
    [SerializeField] TextMeshProUGUI wavesSurvivedText;

    private void Awake()
    {
        _wavesInfo = GetComponent<TextMeshProUGUI>();
    }
    public static void UpdateWaveInfo(int currentWaveIndex, int levelWavesCount)
    {
        _wavesInfo.text = $"WAVE: {currentWaveIndex}/{levelWavesCount}";
    }
}
