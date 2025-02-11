using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PistolTowerController : MonoBehaviour
{
    [SerializeField] private Transform turretRotationPoint;

    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float bps = 1f;

    private float fireRate;
    private float damage = 1;
    private float peirce = 1;
    private float timeUntilFire;

    private Transform target;
    // Start is called before the first frame update
    private void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();
        if(!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            if(timeUntilFire <= 0)
            {
                Shoot(); 
            }
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire >= bps)
            {
                timeUntilFire = 0f;
            }
        }
    }
    private void FindTarget()
    {
    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
    if (hits.Length > 0)
        {
        target = hits[0].transform;
        }
    }
    private bool CheckTargetIsInRange()
        {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
        }
    private void Shoot()
        {
        GameObject bulletobj = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        }
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = targetRotation;
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
