using UnityEngine;

public class One : NumberController, INumberEnemy
{
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private GameObject OneSprite; // The Number1 prefab

    private int level = 1;
    private int originalLevel = 1;
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
        level -= damage;
        if (level <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateSprite();
        }
    }

    public void SpawnNumberOne()
    {
        if (OneSprite != null)
        {
            SpawnNumber(OneSprite); // Inherited from NumberController
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
