using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float score = 0;

    [SerializeField] private Image healthBar;

    public bool gameOver;

    [SerializeField] private AudioClip gameMusic;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GameObject.FindGameObjectsWithTag("AudioManager")[0].GetComponent<AudioSource>();
        audioSource.clip = gameMusic;
        audioSource.priority = 1;
        audioSource.volume = 0.08f;
        audioSource.Play();
    }

    void Start()
    {
        player.OnPlayerDead += GameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        healthBar.fillAmount = player.Lives / 100f;
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(DelayGameOver());
    }

    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1.1f);
        gameOver = true;
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
    }

    public void GoToMenu()
    {
        Destroy(GameObject.FindGameObjectsWithTag("AudioManager")[0]);
        SceneManager.LoadScene("MenuScene");
    }
}
