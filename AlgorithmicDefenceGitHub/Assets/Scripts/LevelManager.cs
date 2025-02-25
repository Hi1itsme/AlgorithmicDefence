using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform[] path;
    public Transform startPoint;

    private bool isSpawning = false;
    private int currentWaveIndex = 0; // Track the current wave index for sequential spawning

    private void Awake()
    {
        main = this;

        if (path == null || path.Length == 0 || startPoint == null)
        {
            Debug.LogError("Path or startPoint not set up in LevelManager!");
        }
    }

    private void Update()
    {
        WaveSpawner();
    }

    private void WaveSpawner()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpawning && currentWaveIndex < 3) // Limit to 3 waves for now
        {
            isSpawning = true;
            Debug.Log($"Starting Wave {currentWaveIndex + 1} manually");
            switch (currentWaveIndex)
            {
                case 0:
                    SpawnWave1();
                    break;
                case 1:
                    SpawnWave2();
                    break;
                case 2:
                    SpawnWave3();
                    break;
                default:
                    Debug.LogError($"Invalid wave index: {currentWaveIndex}");
                    isSpawning = false;
                    return;
            }
        }
    }

    // Private method to spawn Wave 1: 5 Number 1, 3 Number 2, spawn every 1 second
    private void SpawnWave1()
    {
        Debug.Log("Starting Wave 1");
        StartCoroutine(SpawnWave(
            new GameObject[] { Resources.Load<GameObject>("Prefabs/Numbers/1"), Resources.Load<GameObject>("Prefabs/Numbers/2") },
            new int[] { 5, 3 },
            1f
        ));
    }

    // Private method to spawn Wave 2: 4 Number 3, 2 Number 1, spawn every 0.5 seconds
    private void SpawnWave2()
    {
        Debug.Log("Starting Wave 2");
        StartCoroutine(SpawnWave(
            new GameObject[] { Resources.Load<GameObject>("Prefabs/Numbers/3"), Resources.Load<GameObject>("Prefabs/Numbers/1") },
            new int[] { 4, 2 },
            0.5f
        ));
    }

    // Private method to spawn Wave 3: 3 Number 1, 2 Number 2, 1 Number 3, spawn every 0.75 seconds
    private void SpawnWave3()
    {
        Debug.Log("Starting Wave 3");
        StartCoroutine(SpawnWave(
            new GameObject[] { Resources.Load<GameObject>("Prefabs/Numbers/1"), Resources.Load<GameObject>("Prefabs/Numbers/2"), Resources.Load<GameObject>("Prefabs/Numbers/3") },
            new int[] { 3, 2, 1 },
            0.75f // Example delay, adjust as needed
        ));
    }

    // Private coroutine to handle spawning logic (reusable across waves)
    private IEnumerator SpawnWave(GameObject[] prefabs, int[] counts, float delay)
    {
        if (prefabs == null || counts == null || prefabs.Length != counts.Length)
        {
            Debug.LogError("Wave configuration mismatch: prefabs and counts must have the same length!");
            isSpawning = false;
            yield break;
        }

        // Validate prefabs with detailed logging
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i] == null)
            {
                Debug.LogError($"Prefab at index {i} is null! Ensure it exists in Assets/Resources/Prefabs/Numbers/ with the exact name and path (e.g., Prefabs/Numbers/1). Current path checked: Prefabs/Numbers/{(i == 0 ? "1" : i == 1 ? "2" : "3")}");
                isSpawning = false;
                yield break;
            }
        }

        // Spawn each number type according to its count
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i];
            int count = counts[i];

            for (int j = 0; j < count; j++)
            {
                GameObject instance = Instantiate(prefab, startPoint.position, Quaternion.identity);
                EnemyMovement movement = instance.GetComponent<EnemyMovement>();
                if (movement != null)
                {
                    movement.InitializeMovement(); // Use public method for initial spawns
                }
                else
                {
                    Debug.LogError($"EnemyMovement missing on spawned prefab {prefab.name}!");
                }
                yield return new WaitForSeconds(delay);
            }
        }

        isSpawning = false;
        currentWaveIndex++; // Move to the next wave after this one completes
        Debug.Log("Wave completed!");
    }
}