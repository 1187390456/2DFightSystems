using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Move : E_Move
{
    private WildBoar wildBoar;
    public WildBoar_Move(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Move moveData, WildBoar wildBoar) : base(stateMachine, entity, animName, moveData)
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
        if (!entity.isTouchGround || entity.isTouchWall)
        {
            stateMachine.ChangeState(wildBoar.idle);
        }
        if (entity.isCheckPlayer1 && !entity.isCheckPlayer2)
        {
            stateMachine.ChangeState(wildBoar.vigilant);
        }
        if (entity.isCheckPlayer1 && entity.isCheckPlayer2)
        {
            stateMachine.ChangeState(wildBoar.attack);
        }
    }
}
