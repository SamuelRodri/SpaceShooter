using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PowerUpAudio : MonoBehaviour
{
    [SerializeField] private AudioClip touchedAudio;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayTouchedSound(Action OnCompleted = null)
        => PlaySound(touchedAudio, OnCompleted);

    private void PlaySound(AudioClip clip, Action OnComplete = null)
    {
        _audioSource.PlayOneShot(clip);

        if (OnComplete != null)
        {
            StartCoroutine(WaitForSoundToFinish(clip.length, OnComplete));
        }
    }

    private IEnumerator WaitForSoundToFinish(float soundLength, System.Action onComplete)
    {
        yield return new WaitForSeconds(soundLength);
        onComplete.Invoke();
    }
}
