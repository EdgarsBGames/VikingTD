using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Tower towerData;

    public Transform target;
    public string enemyTag = "Enemy";

    public Transform projectileSpawnPoint;
    public float projectileSpeed = 5f;

    private Animator animator;
    private float coolDownTimer;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        InvokeRepeating("FindTarget", 0f, 1f);
    }

    private void Update()
    {
        if (target == null)
            return;
        ShootTarget();
    }
    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy !=null && shortestDistance <= towerData.Range)
        {
            target = nearestEnemy.transform;

        }
        else
        {
            target = null;
            ActivateAttackAnim(false);
        }
    }

    void ShootTarget()
    {
        if(target != null)
        {
            ActivateAttackAnim(true);
            coolDownTimer -= Time.deltaTime;
            if(coolDownTimer <= 0f)
            {
                coolDownTimer = towerData.CoolDown;
                SpawnProjectile();
            }
        }
    }

    void ActivateAttackAnim(bool state)
    {
        if (animator != null)
        {
            animator.SetBool("EnemyFound", state);
        }
        else
        {
            return;
        }
            
    }

    void SpawnProjectile()
    {
        var projectilInstance = Instantiate(towerData.Projectile, projectileSpawnPoint.position, Quaternion.identity);

        projectilInstance.GetComponent<Projectille>().SetupProjectileData(towerData.Damage, towerData.ProjectileSpeed, target.position,towerData.TargetTag); // Inputs all necessary data into projectile
        projectilInstance.GetComponent<Projectille>().projectileSpawnPoint = projectileSpawnPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, towerData.Range);
    }
}
