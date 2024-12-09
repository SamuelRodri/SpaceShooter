using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (FindObjectsOfType<AudioSource>().Length > 1)
        {
            Destroy(audioSource.gameObject);
            return;
        }

        DontDestroyOnLoad(audioSource.gameObject);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
