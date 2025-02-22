using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INumberEnemy 
{
    int GetLevel();
    void TakeDamage(int damage);
}
