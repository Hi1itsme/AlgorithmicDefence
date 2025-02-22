using UnityEngine;

public class Two : NumberController, INumberEnemy
{
    [SerializeField] private Sprite[] numberSprites; // e.g., [sprite_1, sprite_2]
    [SerializeField] private GameObject TwoSprite; // Number2 prefab
    [SerializeField] private GameObject[] lowerNumberPrefabs; // [Number1]

    private int level = 2;
    private int originalLevel = 2;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
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
        for (int i = 0; i < originalLevel; i++) // Spawn 2 Number1s for Number2
        {
            Vector3 offset = Random.insideUnitCircle * 0.5f;
            GameObject newNumber = Instantiate(prefabToSpawn, transform.position + offset, Quaternion.identity);
            // No further splitting needed for Number1 or Number2 in this case
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