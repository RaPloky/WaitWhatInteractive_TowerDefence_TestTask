using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distanceLimit;

    public int destroyReward;

    private Transform target;
    private int waypointIndex = 0;

    private void Start()
    {
        target = Waypoints.waypoints[0];
    }
    private void Update()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= distanceLimit)
            GetNextWaypoint();
    }
    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            WaveSpawner.currentWaveEnemies--;
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }
    private void OnMouseDown()
    {
        DestroyEnemy();
    }
    private void DestroyEnemy()
    {
        CoinManager.coinsCount += destroyReward;
        SetCoinsInfo.UpdateCoinsInfo(CoinManager.coinsCount);
        WaveSpawner.currentWaveEnemies--;
        Destroy(gameObject);
    }
}
