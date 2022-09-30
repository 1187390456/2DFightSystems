using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Dead : E_Dead
{
    private Monster Monster;

    public Monster_Dead(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Dead deadData, Monster Monster) : base(stateMachine, entity, anmName, deadData)
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
        if (isdeadOver)
        {
            stateMachine.ChangeState(Monster.move);
        }
    }
}