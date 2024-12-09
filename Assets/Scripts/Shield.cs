using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject shield;

    private void Update()
    {
        transform.position = player.transform.position;
    }

    public void ActiveShield()
    {
        if (player.Lives <= 0) return;
        GetComponent<CircleCollider2D>().enabled = true;
        shield.gameObject.SetActive(true);
    }

    public void DestroyShield()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        shield.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            DestroyShield();
        }
    }
}
