using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Dodge : E_Dodge
{
    private Archer archer;

    public Archer_Dodge(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Dodge dodgeData, Archer archer) : base(stateMachine, entity, anmName, dodgeData)
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
        if (isDodgeOver)
        {
            stateMachine.ChangeState(archer.remoteAttack);
        }
    }
}