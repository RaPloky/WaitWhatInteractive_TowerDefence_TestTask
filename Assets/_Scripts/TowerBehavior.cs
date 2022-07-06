using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    private Transform _target;
    [SerializeField] float attackRange;
    [SerializeField] string enemyTag;
    [SerializeField] float updateTargetFrequency;
    [SerializeField] float fireRate;
    [SerializeField] int damage;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, updateTargetFrequency);
    }
    private void UpdateTarget()
    {
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
            ShootTarget(_target, damage);
        }
        else
            _target = null;
    }
    private void ShootTarget(Transform target, int damage)
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
}
