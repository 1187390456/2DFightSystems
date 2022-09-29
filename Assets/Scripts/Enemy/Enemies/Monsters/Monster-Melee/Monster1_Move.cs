using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_Move : E_Move
{
    private Monster1 monster1;

    public Monster1_Move(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Move moveData, Monster1 monster1) : base(stateMachine, entity, anmName, moveData)
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
            stateMachine.ChangeState(monster1.detected);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(monster1.idle);
        }
    }
}