using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Idle : E_Idle
{
    private Monster Monster;

    public Monster_Idle(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Idle idleData, Monster Monster) : base(stateMachine, entity, anmName, idleData)
    {
        this.Monster = Monster;
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
            stateMachine.ChangeState(Monster.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(Monster.move);
        }
    }
}