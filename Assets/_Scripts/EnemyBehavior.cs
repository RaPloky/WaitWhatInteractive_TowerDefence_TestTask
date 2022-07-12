using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distanceLimit;

    public int currentHealth;
    public int destroyReward;
    public Image healthBar;

    private Transform _waypointTarget;
    private int _waypointIndex = 0;
    private float _startHealth;

    private void Start()
    {
        _waypointTarget = Waypoints.waypoints[0];
        _startHealth = currentHealth;
    }
    private void Update()
    {
        Vector2 dir = _waypointTarget.position - transform.position;
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);

        if (Vector2.Distance(transform.position, _waypointTarget.position) <= distanceLimit)
            GetNextWaypoint();
    }
    private void GetNextWaypoint()
    {
        if (_waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            DecreaseLives();
            EndPath();
            return;
        }
        _waypointIndex++;
        _waypointTarget = Waypoints.waypoints[_waypointIndex];
    }
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);
        healthBar.fillAmount = currentHealth / _startHealth;

        if (Mathf.Approximately(currentHealth, 0))
            DestroyEnemy();
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
