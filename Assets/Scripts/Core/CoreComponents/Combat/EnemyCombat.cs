using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    public override void CheckKncokBack()
    {
        base.CheckKncokBack();
        if (isKnockbacking)
        {
            if ((movement.rbY <= 0.01f && enemySense.Ground()) || Time.time >= knockbackStartTime + maxKnockbackTime)
            {
                isKnockbacking = false;
                movement.canSetVelocity = true;
            }
        }
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        enemyStats.DecreaseHealth(amount);
    }
}