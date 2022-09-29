using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Dash : E_Dash
{
    private WildBoar wildBoar;
    public WildBoar_Dash(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Dash dashData, WildBoar wildBoar) : base(stateMachine, entity, animName, dashData)
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
    }

    public override void Update()
    {
        base.Update();
        if (entity.isCheckPlayer2)
        {
            stateMachine.ChangeState(wildBoar.attack);
        }
        else if (!entity.isCheckPlayer2 && !entity.isCheckPlayer1)
        {
            stateMachine.ChangeState(wildBoar.move);
        }
    }
}
