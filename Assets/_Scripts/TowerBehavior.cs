using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    private Transform _target;

    [SerializeField] string enemyTag;
    [SerializeField] float attackRange;
    [SerializeField] float fireRate;
    [SerializeField] GameObject projectilePrefab;
    public int damage;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, fireRate);
    }
    private void UpdateTarget()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning($"No projectile for '{gameObject.name}' selected!");
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= attackRange)
        {
            _target = nearestEnemy.transform;
            Instantiate(projectilePrefab, transform);
        }
        else
            _target = null;
    }
    public void DamageTarget(Transform target, int damage)
    {
        int targetHealth = target.GetComponent<EnemyBehavior>().health;
        target.GetComponent<EnemyBehavior>().health = Mathf.Clamp(targetHealth - damage, 0, targetHealth);

        if (target.GetComponent<EnemyBehavior>().health == 0)
            target.GetComponent<EnemyBehavior>().DestroyEnemy();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public Transform GetTarget()
    {
        return _target;
    }
    public string GetTargetTag()
    {
        return enemyTag;
    }
}
