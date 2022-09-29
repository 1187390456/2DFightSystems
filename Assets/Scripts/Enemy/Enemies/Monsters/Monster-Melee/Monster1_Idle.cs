using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_Idle : E_Idle
{
    private Monster1 monster1;

    public Monster1_Idle(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Idle idleData, Monster1 monster1) : base(stateMachine, entity, anmName, idleData)
    {
        this.monster1 = monster1;
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
            SetTurn(false);
            stateMachine.ChangeState(monster1.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(monster1.move);
        }
    }
}