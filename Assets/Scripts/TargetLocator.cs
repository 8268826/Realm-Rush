using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float range = 15f;
    Transform target;

    private void Update()
    {
        FindClosesTarget();
        AimWeappon();
    }

    private void FindClosesTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                maxDistance = targetDistance;
                closestTarget = enemy.transform;
            }
        }
        target = closestTarget;
    }

    private void AimWeappon()
    {
        if(target)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            weapon.LookAt(target);
            if (targetDistance < range) Attack(true);
            else Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
