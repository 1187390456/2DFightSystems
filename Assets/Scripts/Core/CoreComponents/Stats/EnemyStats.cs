using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public override void DecreaseHealth(float amount)
    {
        base.DecreaseHealth(amount);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            enemyState.stateMachine.ChangeState(enemyState.dead);
        }
        else
        {
        }
    }

    public override void IncreaseHealth(float amount)
    {
        base.IncreaseHealth(amount);
    }
}