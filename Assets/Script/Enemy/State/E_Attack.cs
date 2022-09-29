using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Attack : E_State
{
    protected D_E_Attack attackData;
    public E_Attack(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Attack attackData) : base(stateMachine, entity, animName)
    {
        this.attackData = attackData;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
