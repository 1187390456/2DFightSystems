using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Move : E_State
{
    protected D_E_Move moveData; // 移动数据

    public E_Move(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Move moveData) : base(stateMachine, entity, anmName)
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

    public override void FixUpdate()
    {
        base.FixUpdate();
        movement.SetVelocityX(moveData.moveSpeed);
    }

    public override void Update()
    {
        base.Update();
    }
}