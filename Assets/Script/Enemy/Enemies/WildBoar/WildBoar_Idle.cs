using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Idle : E_Idle
{
    private WildBoar wildBoar;
    public WildBoar_Idle(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Idle idleData, WildBoar wildBoar) : base(stateMachine, entity, animName, idleData)
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

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isIdleTimeOver)
        {
            entity.stateMachine.ChangeState(wildBoar.move);
        }
    }

    public override void Update()
    {
        base.Update();

    }
}
