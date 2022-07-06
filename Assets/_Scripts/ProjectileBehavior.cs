using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    private TowerBehavior _tower;
    private Transform _target;

    private void Awake()
    {
        _tower = transform.parent.GetComponent<TowerBehavior>();
        _target = _tower.GetTarget();
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        _tower.DamageTarget(_target, _tower.damage);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 dir = _target.position - transform.position;
        transform.Translate(projectileSpeed * Time.deltaTime * dir.normalized, Space.World);
    }
}
