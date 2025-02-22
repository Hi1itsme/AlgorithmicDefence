using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject onePrefab; // Assign Number1 prefab
    [SerializeField] private GameObject twoPrefab; // Assign Number2 prefab

    public static LevelManager main;
    public Transform[] path;
    public Transform startPoint;

    private bool isSpawning = false;
    private int waveCount = 0;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        if (onePrefab == null || twoPrefab == null)
        {
            Debug.LogError("OnePrefab or TwoPrefab not assigned in LevelManager!");
        }
    }

    private void Update()
    {
        WaveSpawner();
    }

    private void WaveSpawner()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSpawning)
        {
            waveCount++;
            isSpawning = true;
            Debug.Log($"Starting Wave {waveCount}");
            StartCoroutine(RunWave());
        }
    }

    private IEnumerator RunWave()
    {
        if (waveCount == 1)
        {
            yield return StartCoroutine(Wave1());
        }
        else if (waveCount == 2)
        {
            yield return StartCoroutine(Wave2());
        }
        isSpawning = false;
        Debug.Log($"Wave {waveCount} completed!");
    }

    private IEnumerator Wave1()
    {
        for (int i = 0; i < 14; i++)
        {
            Instantiate(onePrefab, startPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Wave2()
    {
        for (int i = 0; i < 14; i++)
        {
            Instantiate(twoPrefab, startPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}