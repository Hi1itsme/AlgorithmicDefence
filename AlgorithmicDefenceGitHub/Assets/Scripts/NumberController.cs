using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DealDamage(float damage)
    {
        
    }
    public void SpawnNumber(GameObject number)
    {
        Instantiate(number, LevelManager.main.startPoint.position, Quaternion.identity);

    }
}
