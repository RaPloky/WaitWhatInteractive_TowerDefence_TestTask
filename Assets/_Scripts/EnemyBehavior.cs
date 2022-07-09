using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distanceLimit;

    public int health;
    public int destroyReward;

    private Transform waypointTarget;
    private int waypointIndex = 0;

    private void Start()
    {
        waypointTarget = Waypoints.waypoints[0];
    }
    private void Update()
    {
        Vector2 dir = waypointTarget.position - transform.position;
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);

        if (Vector2.Distance(transform.position, waypointTarget.position) <= distanceLimit)
            GetNextWaypoint();
    }
    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            DecreaseLives();
            EndPath();
            return;
        }
        waypointIndex++;
        waypointTarget = Waypoints.waypoints[waypointIndex];
    }
    public void DestroyEnemy()
    {
        AddRewardForEnemy();
        WaveSpawner.currentWaveEnemies--;
        Destroy(gameObject);
    }
    private void AddRewardForEnemy()
    {
        PlayerStats.Money += destroyReward;
        PlayerStats.instance.UpdateMoneyText();
    }
    private void DecreaseLives()
    {
        PlayerStats.AmountOfLives = Mathf.Clamp(PlayerStats.AmountOfLives - 1, 0, PlayerStats.instance.livesLimit);
        PlayerStats.instance.UpdateLivesAmount();
    }
    private void EndPath()
    {
        WaveSpawner.currentWaveEnemies--;
        Destroy(gameObject);
    }
}
