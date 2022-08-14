using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectille : MonoBehaviour
{
    public float projectileDamage;
    public float projectileSpeed;
    public Transform projectileSpawnPoint;

    public string TargetTag;

    public Vector3 target;

    private Quaternion lookRotation;
    private Vector3 direction;
    //public Vector3 targetPosition;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        MoveProjectile();
    }

    public void SetupProjectileData(float damage,float speed,Vector3 targetPos,string tag)
    {
        projectileDamage = damage;
        projectileSpeed = speed;
        TargetTag = tag;

        target = targetPos;
    }

    public void MoveProjectile(Vector3 targetPosition)
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        transform.position += moveDirection * projectileSpeed * Time.deltaTime;

    }

    void HandleRotation(Vector3 targetPosition)
    {
        direction = targetPosition - transform.position; // find direction

        lookRotation = Quaternion.LookRotation(direction); //creates rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 50f);//rotates character

    }

    void RotateVariatn()
    {
        Vector3 dir = target.normalized - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public virtual void MoveProjectile()
    {
        MoveProjectile(target);
        HandleRotation(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TargetTag)
        {
            other.GetComponent<EnemyController>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
