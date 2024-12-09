using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    protected override void MakeEffect(Player player)
    {
        var shield = FindObjectOfType<Shield>();
        shield.ActiveShield();
    }
}
