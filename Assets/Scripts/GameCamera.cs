using DG.Tweening;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private float duration;
    [SerializeField] private float strength;

    private void Start()
    {
        player.OnPlayerHit += CameraShake;
    }

    public void CameraShake()
    {
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }
}
