using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShipAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _shootAudio;
    [SerializeField] private AudioClip _hitAudio;
    [SerializeField] private AudioClip _destroyAudio;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootAudio(System.Action OnComplete = null)
        => PlaySound(_shootAudio, OnComplete);

    public void PlayHitAudio(System.Action OnComplete = null)
        => PlaySound(_hitAudio, OnComplete);

    public void PlayDestroyAudio(System.Action OnComplete = null)
        => PlaySound(_destroyAudio, OnComplete);

    private void PlaySound(AudioClip clip, System.Action OnComplete = null)
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
