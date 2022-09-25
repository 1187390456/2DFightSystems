using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Move : E_Move
{
    private WildBoar wildBoar;

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
    }

    public override void Update()
    {
        base.Update();
        if (entity.CheckMinDetected())
        {
            stateMachine.ChangeState(wildBoar.detected);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(wildBoar.idle);
        }
    }
}