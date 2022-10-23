using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_Idle : E_Idle
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_Idle(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Idle idleData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, idleData)
    {
        this.meleeMonster = meleeMonster;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MinDetected())
        {
            SetTurn(false);
            stateMachine.ChangeState(meleeMonster.detected);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(meleeMonster.move);
        }
    }
}