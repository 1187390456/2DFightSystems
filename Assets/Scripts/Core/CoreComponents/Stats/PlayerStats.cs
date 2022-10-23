using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public override void DecreaseHealth(float amount)
    {
        base.DecreaseHealth(amount);
        if (currentHealth <= 0)
        {
            ClearHealth();
            playerState.stateMachine.ChangeState(playerState.dead);
        }
        else
        {
        }
    }
}