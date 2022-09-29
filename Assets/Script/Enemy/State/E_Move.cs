using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Move : E_State
{
    protected D_E_Move moveData;
    public E_Move(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Move moveData) : base(stateMachine, entity, animName)
    {
        this.moveData = moveData;
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
        entity.SetVelocityX(moveData.moveSpeed);
    }

    public override void Update()
    {
        base.Update();
    }
}
