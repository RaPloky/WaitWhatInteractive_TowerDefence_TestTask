using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distanceLimit;

    public int currentHealth;
    public int destroyReward;
    public Image healthBar;

    [SerializeField] GameObject deathEffect;
    private Transform _waypointTarget;
    private int _waypointIndex = 0;
    private float _startHealth;
    private const float DESTROY_EFFECT_DELAY = 2f;

    private void Start()
    {
        // Enemy will start their move to first waypoint in array of waypoints
        _waypointTarget = Waypoints.waypoints[0];
        // Sets for proper healthbar filling value
        _startHealth = currentHealth;
    }
    private void Update()
    {
        Vector2 dir = _waypointTarget.position - transform.position;
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);
        // Change waypoint
        if (Vector2.Distance(transform.position, _waypointTarget.position) <= distanceLimit)
            GetNextWaypoint();
    }
    private void GetNextWaypoint()
    {
        if (IsEnemyReachedPathEnd())
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
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, DESTROY_EFFECT_DELAY);

        AddRewardForEnemy();
        WaveSpawner.instance.currentWaveEnemies--;
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
        WaveSpawner.instance.currentWaveEnemies--;
        Destroy(gameObject);
    }
    private bool IsEnemyReachedPathEnd()
    {
        return _waypointIndex >= Waypoints.waypoints.Length - 1;
    }
}
