using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public abstract class PowerUp : PooledObject
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private PowerUpAudio audioSystem;

    private void Start()
    {
        audioSystem = GetComponent<PowerUpAudio>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSystem.PlayTouchedSound(OnFinishTouchedAudio);
            spriteRenderer.enabled = false;
            MakeEffect(collision.GetComponent<Player>());
        }
    }

    protected abstract void MakeEffect(Player player);

    private void OnFinishTouchedAudio()
    {
        DestroyPooled();
    }
}
