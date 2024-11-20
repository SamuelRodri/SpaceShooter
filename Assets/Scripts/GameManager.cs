using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject gameOverPanel;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        player.OnPlayerDead += GameOver;
    }

    private void Update()
    {
        if (Input.anyKey && gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(DelayGameOver());
    }

    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(2f);
        gameOver = true;
    }
}
