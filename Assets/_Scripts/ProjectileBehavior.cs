using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject hitEffect;
    private TowerBehavior _tower;
    private Transform _target;
    private const float DESTROY_EFFECT_DELAY = 2f;

    private void Awake()
    {
        _tower = transform.parent.GetComponent<TowerBehavior>();
        _target = _tower.GetTarget();
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        _tower.DamageTarget(_target, _tower.damage);
        GameObject effect = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, DESTROY_EFFECT_DELAY);
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
