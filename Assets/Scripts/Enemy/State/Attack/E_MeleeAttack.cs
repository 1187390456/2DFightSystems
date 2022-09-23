using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MeleeAttack : E_Attack
{
    protected D_E_MeleeAttack meleeAttackData; // 近战攻击数据

    public E_MeleeAttack(E_StateMachine stateMachine, E_Entity entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData) : base(stateMachine, entity, anmName, attackPos)
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
        AttackInfo attackInfo = new AttackInfo()
        {
            damage = meleeAttackData.meleeAttackDamage,
            damageSourcePosX = entity.aliveGobj.transform.position.x,
        };
        var obj = Physics2D.OverlapCircle(entity.meleeAttackCheck.position, meleeAttackData.meleeAttackRadius, LayerMask.GetMask("Player"));
        if (obj != null)
        {
            obj.transform.SendMessage("AcceptMeleeAttackDamage", attackInfo);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}