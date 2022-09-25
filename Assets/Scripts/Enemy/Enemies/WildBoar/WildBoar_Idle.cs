using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Idle : E_Idle
{
    private WildBoar wildBoar;

    public WildBoar_Idle(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Idle idleData, WildBoar wildBoar) : base(stateMachine, entity, anmName, idleData)
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
    }

    public override void Update()
    {
        base.Update();
        if (entity.CheckMinDetected())
        {
            // 阻止转身
            SetTurn(false);
            stateMachine.ChangeState(wildBoar.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(wildBoar.move);
        }
    }
}