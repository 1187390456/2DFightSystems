using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Move : E_Move
{
    public Archer archer;

    public Archer_Move(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Move moveData, Archer archer) : base(stateMachine, entity, anmName, moveData)
    {
        this.archer = archer;
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
        if (entity.CheckMaxDetected())
        {
            stateMachine.ChangeState(archer.detected);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(archer.idle);
        }
    }
}