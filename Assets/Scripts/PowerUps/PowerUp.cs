using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    void Update()
    {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MakeEffect(collision.GetComponent<Player>());
        }
    }

    protected abstract void MakeEffect(Player player);
}
