using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start(int? initialPathIndex = null)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                rb.freezeRotation = true;
            }
        }

        if (LevelManager.main == null || LevelManager.main.path == null || LevelManager.main.path.Length == 0)
        {
            Debug.LogError("LevelManager.main.path is not set up!");
            return;
        }

        if (initialPathIndex.HasValue)
        {
            // Use the provided path index (for spawned enemies, move to next checkpoint)
            pathIndex = initialPathIndex.Value;
            if (pathIndex < LevelManager.main.path.Length - 1)
            {
                pathIndex++; // Move to the next checkpoint
                target = LevelManager.main.path[pathIndex];
                Debug.Log($"Spawned enemy {gameObject.name} starting at path index {pathIndex} (position: {transform.position}, target: {target.position})");
            }
            else
            {
                // If already at or past the last waypoint, destroy the enemy (optional behavior)
                Debug.LogWarning($"Spawned enemy {gameObject.name} at or beyond last path index, destroying.");
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            // For initial spawns (e.g., from LevelManager), find the nearest waypoint
            float closestDistance = float.MaxValue;
            int closestIndex = 0;
            float minDistanceThreshold = 0.01f; // Ignore very close waypoints

            for (int i = 0; i < LevelManager.main.path.Length; i++)
            {
                float distance = Vector2.Distance(transform.position, LevelManager.main.path[i].position);
                if (distance < minDistanceThreshold) continue;
                Debug.Log($"Checking waypoint {i} at {LevelManager.main.path[i].position}, distance: {distance}");
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            pathIndex = closestIndex;
            target = LevelManager.main.path[pathIndex];
            Debug.Log($"Initial enemy {gameObject.name} starting at path index {pathIndex} (position: {transform.position}, target: {target.position})");
        }
    }

    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning($"Target is null for {gameObject.name}, reinitializing...");
            Start(); // Reinitialize with nearest waypoint (remove in production if not needed)
            return;
        }

        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex >= LevelManager.main.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
                Debug.Log($"Moved to path index {pathIndex}, target: {target.position}");
            }
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            Debug.LogWarning($"No target in FixedUpdate for {gameObject.name}");
        }
    }
}