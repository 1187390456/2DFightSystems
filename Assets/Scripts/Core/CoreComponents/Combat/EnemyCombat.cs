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
        if (StopCondition()) return;
        enemyStats.DecreaseHealth(amount);
    }

    public override void Knckback(float velocity, Vector2 angle, int direction)
    {
        base.Knckback(velocity, angle, direction);
        if (StopCondition()) return;
        Debug.Log("123");
        movement.SetVelocity(velocity, angle, direction);
        knockbackStartTime = Time.time;
        movement.canSetVelocity = false;
        isKnockbacking = true;
    }

    private bool StopCondition() => target.GetComponent<Enemy>().ablity2ing || target.GetComponent<Enemy>().deading;
}