using UnityEngine;

public class BULLETCONTROLLER : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 1; // Configurable in Inspector

    private Vector3 direction;

    private void Start()
    {
        // Bullet inherits rotation from firing point
        direction = transform.up; // Assumes sprite faces up
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        INumberEnemy enemy = other.GetComponent<INumberEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // Destroy bullet on hit
        }
    }
}
