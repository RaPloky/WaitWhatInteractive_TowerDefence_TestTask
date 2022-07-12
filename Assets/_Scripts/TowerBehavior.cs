using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    [HideInInspector] public Transform Target { get; set; }

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
            Target = nearestEnemy.transform;
            Instantiate(projectilePrefab, transform);
        }
        else
            Target = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public string GetTargetTag()
    {
        return enemyTag;
    }
}
