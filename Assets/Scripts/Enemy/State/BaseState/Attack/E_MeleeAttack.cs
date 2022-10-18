using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MeleeAttack : E_Attack
{
    protected D_E_MeleeAttack meleeAttackData;

    public E_MeleeAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData) : base(stateMachine, entity, anmName, attackPos)
    {
        this.meleeAttackData = meleeAttackData;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void StartAttack()
    {
        base.StartAttack();
        var damageObjs = Physics2D.OverlapCircleAll(sense.meleeAttackCheck.position, sense.meleeAttackRadius, LayerMask.GetMask("Player"));
        foreach (var item in damageObjs)
        {
            IDamageable damageable = item.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(meleeAttackData.meleeAttackDamage);
            }
            IKnockbackable knockbackable = item.GetComponent<IKnockbackable>();
            if (knockbackable != null)
            {
                knockbackable.Knckback(meleeAttackData.knockbackSpeed, meleeAttackData.knockbackAngle, movement.facingDireciton);
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }
}