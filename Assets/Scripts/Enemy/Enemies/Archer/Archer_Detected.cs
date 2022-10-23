using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Detected : E_Detected
{
    protected Archer archer;

    public Archer_Detected(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Detected detectedData, Archer archer) : base(stateMachine, entity, anmName, detectedData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (isDetectedOver)
        {
            if (sense.MeleeAttack())
            {
                if (archer.DodgeCondition())
                {
                    stateMachine.ChangeState(archer.dodge);
                }
                else
                {
                    stateMachine.ChangeState(archer.meleeAttack);
                }
            }
            else if (sense.MinDetected())
            {
                stateMachine.ChangeState(archer.remoteAttack);
            }
            else if (sense.MaxDetected())
            {
                stateMachine.ChangeState(archer.ability1);
            }
            else
            {
                stateMachine.ChangeState(archer.findPlayer);
            }
        }
    }
}