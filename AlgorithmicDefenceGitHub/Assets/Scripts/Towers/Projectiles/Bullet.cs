using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private Rigidbody2D barrelRb;

    [SerializeField] private float bulletSpeed = 5f;
    private void Awake()
    {
        barrelRb =
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        bulletRb.transform.rotation = barrelRb.transform.rotation;
        bulletRb.velocity = transform.up * bulletSpeed;
    }
}
