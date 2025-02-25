using UnityEngine;

public class Two : NumberController, INumberEnemy
{
    [SerializeField] private Sprite[] numberSprites; // e.g., [sprite_1, sprite_2]
    [SerializeField] private GameObject TwoSprite; // Number2 prefab
    [SerializeField] private GameObject[] lowerNumberPrefabs; // [Number1]

    private int level = 2;
    private int originalLevel = 2;
    private SpriteRenderer spriteRenderer;
    private EnemyMovement enemyMovement; // To access pathIndex

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        enemyMovement = GetComponent<EnemyMovement>(); // Get the movement component
        if (enemyMovement == null)
        {
            Debug.LogError($"EnemyMovement missing on {gameObject.name}!");
        }
        UpdateSprite();
    }

    public int GetLevel()
    {
        return level;
    }

    public void TakeDamage(int damage)
    {
        SplitIntoLowerNumbers(damage);
        Destroy(gameObject);
    }

    public void SpawnNumberTwo()
    {
        if (TwoSprite != null)
        {
            SpawnNumber(TwoSprite);
        }
    }

    private void SplitIntoLowerNumbers(int damage)
    {
        int newLevel = level - damage;
        if (newLevel <= 0)
        {
            newLevel = 1; // Ensure we spawn Number1s if damage exceeds level
        }

        if (newLevel - 1 >= lowerNumberPrefabs.Length || newLevel - 1 < 0)
        {
            Debug.LogError($"Invalid index for lowerNumberPrefabs at level {newLevel}. Array size: {lowerNumberPrefabs.Length}");
            return;
        }

        GameObject prefabToSpawn = lowerNumberPrefabs[newLevel - 1]; // Should be Number1 for newLevel = 1
        int parentPathIndex = enemyMovement != null ? enemyMovement.CurrentPathIndex : 0; // Use parent's path index via property
        Debug.Log($"Splitting Number2 at path index {parentPathIndex}, spawning {originalLevel} Number1s");

        for (int i = 0; i < originalLevel; i++) // Spawn 2 Number1s for Number2
        {
            Vector3 offset = Random.insideUnitCircle * 0.5f;
            GameObject newNumber = Instantiate(prefabToSpawn, transform.position + offset, Quaternion.identity);
            EnemyMovement newMovement = newNumber.GetComponent<EnemyMovement>();
            if (newMovement != null)
            {
                newMovement.InitializeMovement(parentPathIndex); // Use public method for spawned enemies
            }
            else
            {
                Debug.LogError($"EnemyMovement missing on spawned prefab {prefabToSpawn.name}");
            }
        }
    }

    private void UpdateSprite()
    {
        if (spriteRenderer != null && numberSprites != null && level > 0 && level <= numberSprites.Length)
        {
            spriteRenderer.sprite = numberSprites[level - 1];
        }
    }
}