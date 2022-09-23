using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Attack : E_State
{
    protected Transform attackPos; // 攻击位置
    protected bool isFinshAttack; //  攻击是否结束

    public E_Attack(E_StateMachine stateMachine, E_Entity entity, string anmName, Transform attackPos) : base(stateMachine, entity, anmName)
    {
        this.attackPos = attackPos;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0.0f);
        isFinshAttack = false;
        entity.animationToScript.attackState = this;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
    }

    public virtual void StartAttack()
    {
    }

    public virtual void FinishAttack()
    {
        isFinshAttack = true;
    }
}