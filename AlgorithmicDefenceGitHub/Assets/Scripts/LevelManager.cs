using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    One oneScript;

    public static LevelManager main;
    public Transform[] path;
    public Transform startPoint;

    private bool isSpawning = false;
    private bool oneTimerActive = false;
    
    private float timeSinceLastSpawn;
    private float enemyRate;
    private float enemiesLeftToSpawn;
    private float oneTiming;

    private int WaveCount;

    private string WaveCountName;
    private void Awake()
    {
        main = this;  
    }
    private void Start()
    {
        oneScript = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<One>();
        
    }

    // Update is called once per frame
    void Update()
    {

        WaveSpawner();
    }
     private void WaveSpawner()
    {
        if(Input.GetKeyDown("space"))
        {
            isSpawning = true;
        }
        if(isSpawning)
        {
            WaveCount++;
        }

        
        if(isSpawning && WaveCount == 1)
        {
            StartCoroutine(Wave1());
        }
        
        
    }
    IEnumerator Wave1()
    {
        for(int i = 1; i < 15; i ++)
        {
            isSpawning = true;
            oneScript.SpawnNumberOne();
            yield return new WaitForSeconds(1);
        }
        
    }
}
