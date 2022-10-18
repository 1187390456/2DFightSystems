using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [Header("最大击退时长")] public float maxKnockbackTime = 0.2f;
    protected bool isKnockbacking;
    protected float knockbackStartTime;

    public virtual void Damage(float amount)
    {
    }

    public virtual void CheckKncokBack()
    {
    }

    public void Knckback(float velocity, Vector2 angle, int direction)
    {
        movement.SetVelocity(velocity, angle, direction);
        knockbackStartTime = Time.time;
        movement.canSetVelocity = false;
        isKnockbacking = true;
    }

    public override void Update()
    {
        base.Update();
        CheckKncokBack();
    }
}