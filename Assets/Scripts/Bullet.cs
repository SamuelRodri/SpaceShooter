using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 direction;

    private bool canShoot = true;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);    
    }
}
