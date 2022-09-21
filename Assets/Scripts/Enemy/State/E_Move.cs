using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Move : E_State
{
    protected D_E_Move moveData; // 移动数据
    protected bool isMinDetected; // 是否进入最小警备距离

    public E_Move(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Move moveData) : base(stateMachine, entity, anmName)
    {
        this.moveData = moveData;
    }

    public override void Enter()
    {
        base.Enter();
        isMinDetected = entity.CheckMinDetected();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
        entity.SetVelocity(moveData.moveSpeed);
    }

    public override void Update()
    {
        base.Update();
        isMinDetected = entity.CheckMinDetected();
    }
}