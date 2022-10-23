using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Move : E_Move
{
    protected Archer archer;

    public Archer_Move(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Move moveData, Archer archer) : base(stateMachine, entity, anmName, moveData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MaxDetected())
        {
            stateMachine.ChangeState(archer.detected);
        }
        else if (sense.Protect())
        {
            stateMachine.ChangeState(archer.idle);
        }
    }
}