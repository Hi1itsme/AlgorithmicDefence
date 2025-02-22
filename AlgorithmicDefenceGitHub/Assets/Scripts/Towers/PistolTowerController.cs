using UnityEditor;
using UnityEngine;

public class PistolTowerController : MonoBehaviour
{
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f;

    private float timeUntilFire;
    private Transform target;

    private void Update()
    {
        if (target == null || !CheckTargetIsInRange())
        {
            FindTarget();
            if (target == null) return;
        }

        RotateTowardsTarget();

        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / bps)
        {
            Shoot();
            timeUntilFire = 0f;
        }
    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);
        Debug.Log($"Found {hits.Length} colliders in range.");
        foreach (var hit in hits)
        {
            INumberEnemy enemy = hit.GetComponent<INumberEnemy>();
            Debug.Log($"Checking collider {hit.name}, INumberEnemy: {(enemy != null ? "Found" : "Not Found")}");
            if (enemy != null)
            {
                target = hit.transform;
                Debug.Log($"Target acquired: {hit.name} at level {enemy.GetLevel()}");
                break;
            }
        }
    }

    private bool CheckTargetIsInRange()
    {
        if (target == null) return false;
        bool inRange = Vector2.Distance(target.position, transform.position) <= targetingRange;
        if (!inRange) Debug.Log($"Target {target.name} out of range.");
        return inRange;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    private void RotateTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        turretRotationPoint.rotation = Quaternion.RotateTowards(
            turretRotationPoint.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, Vector3.forward, targetingRange);
    }
}