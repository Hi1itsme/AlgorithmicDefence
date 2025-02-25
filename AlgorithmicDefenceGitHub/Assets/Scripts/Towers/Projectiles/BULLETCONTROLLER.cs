using UnityEngine;

public class BULLETCONTROLLER : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1; // Configurable in Inspector

    private Vector3 direction;

    private void Start()
    {
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
            Destroy(gameObject); // Destroy bullet after hitting the first enemy
        }
        else
        {
            // Optional: Destroy if hitting non-enemy objects (e.g., walls)
            Destroy(gameObject);
        }
    }
}