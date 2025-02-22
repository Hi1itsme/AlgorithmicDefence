using UnityEngine;

public class NumberController : MonoBehaviour
{
    void Start()
    {
        if (LevelManager.main == null || LevelManager.main.startPoint == null)
        {
            Debug.LogError("LevelManager.main or startPoint is not set up!");
        }
    }

    void Update()
    {
        // Empty for now
    }

    public void SpawnNumber(GameObject number)
    {
        if (number != null)
        {
            Instantiate(number, LevelManager.main.startPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Attempted to spawn a null number prefab!");
        }
    }
}