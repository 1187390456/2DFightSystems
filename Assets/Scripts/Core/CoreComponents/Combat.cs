using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    private bool isKnockbacking;

    public void Damage(float amount)
    {
    }

    public void Knckback(float velocity, Vector2 angle, int direction)
    {
        movement.SetVelocity(velocity, angle, direction);
        movement.canSetVelocity = false;
        isKnockbacking = true;
    }

    public override void Update()
    {
        base.Update();
        CheckKncokBack();
    }

    private void CheckKncokBack()
    {
        if (target.GetComponent<Enemy>() != null)
        {
            if (isKnockbacking && movement.rbY <= 0.01f && enemySense.Ground())
            {
                isKnockbacking = false;
                movement.canSetVelocity = true;
            }
        }
        if (target.GetComponent<Player>() != null)
        {
            if (isKnockbacking && movement.rbY <= 0.01f && playerSense.Ground())
            {
                isKnockbacking = false;
                movement.canSetVelocity = true;
            }
        }
    }
}