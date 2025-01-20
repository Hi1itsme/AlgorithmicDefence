using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One : MonoBehaviour
{
    public GameObject OneSprite;
    public int lvl1 =  1;
    // Start is called before the first frame update
    NumberController numberControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        numberControllerScript = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<NumberController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnNumberOne()
    {
        numberControllerScript.SpawnNumber(OneSprite);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
