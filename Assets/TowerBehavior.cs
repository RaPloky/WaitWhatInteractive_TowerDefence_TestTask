using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    private Transform _target;
    [SerializeField] float attackRange;
    [SerializeField] string enemyTag;
    [SerializeField] float updateTargetFrequency;

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
            _target = nearestEnemy.transform;
        else
            _target = null;
    }
    private void Update()
    {
        if (_target == null)
            return;


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
