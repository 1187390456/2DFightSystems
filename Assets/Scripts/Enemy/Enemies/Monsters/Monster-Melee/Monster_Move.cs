using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : E_Move
{
    private Monster Monster;

    public Monster_Move(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Move moveData, Monster Monster) : base(stateMachine, entity, anmName, moveData)
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
            stateMachine.ChangeState(Monster.detected);
        }
        else if (entity.IsProtect())
        {
            stateMachine.ChangeState(Monster.idle);
        }
    }
}