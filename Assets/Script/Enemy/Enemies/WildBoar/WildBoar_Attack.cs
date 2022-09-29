using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Attack : E_Attack
{
    private WildBoar wildBoar;
    public WildBoar_Attack(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Attack attackData, WildBoar wildBoar) : base(stateMachine, entity, animName, attackData)
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
        if (entity.isCheckPlayer1 && !entity.isCheckPlayer2)
        {
            stateMachine.ChangeState(wildBoar.dash);
        }
        if (!entity.isCheckPlayer1 && !entity.isCheckPlayer2)
        {
            stateMachine.ChangeState(wildBoar.move);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
