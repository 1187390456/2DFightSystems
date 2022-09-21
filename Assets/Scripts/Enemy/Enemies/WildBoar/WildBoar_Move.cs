using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Move : E_Move
{
    private WildBoar wildBoar; // 野猪

    public WildBoar_Move(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Move moveData, WildBoar wildBoar) : base(stateMachine, entity, anmName, moveData)
    {
        this.wildBoar = wildBoar;
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
        if (isMinDetected)
        {
            stateMachine.ChangeState(wildBoar.detected);
        }
        else if (!entity.CheckEdge() || entity.CheckWall())
        {
            // 修改为空闲状态
            stateMachine.ChangeState(wildBoar.idle);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}