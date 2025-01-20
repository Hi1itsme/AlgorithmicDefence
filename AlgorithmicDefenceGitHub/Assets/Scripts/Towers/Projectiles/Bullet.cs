using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRb;

    [SerializeField] private float bulletSpeed = 5f;
    private void Awake()
    {
        
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        bulletRb.velocity = transform.up * bulletSpeed;
    }
}
