using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int currentWaveEnemies;
    [Header("Game Objects")]
    [SerializeField] Transform enemyPrefab;
    [SerializeField] Transform spawnStart;
    [Header("Delays")]
    [SerializeField] [Range(0, 10)] int firstWaveDelay;
    [SerializeField] [Range(3, 10)] int regularWaveDelay;
    [SerializeField] [Range(1f, 5f)] float inwaveSpawnDelay;
    [SerializeField] [Range(0f, 1f)] float checkDelay;
    [Header("Enemies Count")]
    [SerializeField] [Range(1, 20)] int firstWaveEnemies;
    [SerializeField] [Range(1, 50)] int regularWaveEnemies;

    private bool _isAllWaveDefeated = false;

    private void Start() => StartCoroutine(SpawnWave(firstWaveDelay, firstWaveEnemies));
    private IEnumerator SpawnWave(float waveDelay, int enemiesCount)
    {
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
            yield return new WaitForSeconds(checkDelay);
            if (IsWaveDefeated())
            {
                _isAllWaveDefeated = false;
                StartCoroutine(SpawnWave(regularWaveDelay, regularWaveEnemies));
                yield break;
            }
        }
    }
    private bool IsWaveDefeated()
    {
        if (Mathf.Approximately(currentWaveEnemies, 0))
            _isAllWaveDefeated = true;
        return _isAllWaveDefeated;
    }
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnStart.position, spawnStart.rotation);
        currentWaveEnemies++;
    }
}
