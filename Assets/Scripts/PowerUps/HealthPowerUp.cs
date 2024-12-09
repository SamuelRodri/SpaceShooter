using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUp
{
    [SerializeField] private float HealthAmount;

    protected override void MakeEffect(Player player)
    {
        player.Lives += HealthAmount;
    }
}
