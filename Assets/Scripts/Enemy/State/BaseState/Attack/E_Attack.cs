using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Attack : E_State
{
    protected Transform attackPos;
    protected bool isFinshAttack;

    public E_Attack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos) : base(stateMachine, entity, anmName)
    {
        this.attackPos = attackPos;
    }

    public override void Enter()
    {
        base.Enter();
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
        movement.SetVelocityX(0.0f);
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