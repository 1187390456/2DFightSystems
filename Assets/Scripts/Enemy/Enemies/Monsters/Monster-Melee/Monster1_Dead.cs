using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_Dead : E_Dead
{
    private Monster1 monster1;

    public Monster1_Dead(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Dead deadData, Monster1 monster1) : base(stateMachine, entity, anmName, deadData)
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
        if (isdeadOver)
        {
            stateMachine.ChangeState(monster1.move);
        }
    }
}