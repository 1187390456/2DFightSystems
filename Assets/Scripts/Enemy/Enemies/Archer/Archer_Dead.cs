using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Dead : E_Dead
{
    protected Archer archer;

    public Archer_Dead(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dead deadData, Archer archer) : base(stateMachine, entity, anmName, deadData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (isdeadOver)
        {
            stateMachine.ChangeState(archer.move);
        }
    }
}