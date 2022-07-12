using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Singleton
    public static WaveSpawner instance;
    [HideInInspector] public int currentWaveEnemies;
    [HideInInspector] public int currentWaveIndex = 1;

    [Header("Game Objects")]
    [SerializeField] Transform enemyPrefab;
    [SerializeField] Transform spawnStart;

    [Header("Delays")]
    [SerializeField] [Range(0, 10)] int firstWaveDelay;
    [SerializeField] [Range(3, 10)] int regularWaveDelay;
    [SerializeField] [Range(1f, 5f)] float inwaveSpawnDelay;
    [SerializeField] [Range(0f, 1f)] float checkIfWaveDefeatedDelay;

    [Header("Enemies Count")]
    [SerializeField] [Range(1, 20)] int firstWaveEnemies;
    [SerializeField] [Range(1, 50)] int regularWaveEnemies;
    [SerializeField] [Range(0, 5)] int incrementPerWave;

    [Header("Waves")]
    [Range(1, 20)] public int levelWavesCount;

    private bool _isCurrentWaveDefeated = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one manager!");
            return;
        }
        instance = this;
    }
    private void Start() => StartCoroutine(SpawnWave(firstWaveDelay, firstWaveEnemies));
    private IEnumerator SpawnWave(float waveDelay, int enemiesCount)
    {
        PlayerStats.instance.UpdateWaveInfo(currentWaveIndex, levelWavesCount);
        yield return new WaitForSeconds(waveDelay);
        for (int i = 0; i < enemiesCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(inwaveSpawnDelay);
        }
        StartCoroutine(SpawnNextWave());
    }
    private IEnumerator SpawnNextWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkIfWaveDefeatedDelay);
            if (IsWaveDefeated())
            {
                PlayerStats.WavesSurvived++;
                if (IsAllWavesDefeated())
                    yield break;

                currentWaveIndex++;
                // For new wave
                _isCurrentWaveDefeated = false;
                regularWaveEnemies += incrementPerWave;
                StartCoroutine(SpawnWave(regularWaveDelay, regularWaveEnemies));
                yield break;
            }
        }
    }
    private bool IsAllWavesDefeated()
    {
        return Mathf.Approximately(PlayerStats.WavesSurvived, levelWavesCount);
    }
    private bool IsWaveDefeated()
    {
        if (Mathf.Approximately(currentWaveEnemies, 0))
            _isCurrentWaveDefeated = true;
        return _isCurrentWaveDefeated;
    }
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnStart.position, spawnStart.rotation);
        currentWaveEnemies++;
    }
}
