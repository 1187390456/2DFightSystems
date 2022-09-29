using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Dash : E_State
{
    protected D_E_Dash dashData;
    public E_Dash(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Dash dashData) : base(stateMachine, entity, animName)
    {
        this.dashData = dashData;
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

        entity.SetVelocityX(dashData.dashSpeed);
    }

    public override void Update()
    {
        base.Update();
    }
}
