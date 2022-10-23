using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_Detected : E_Detected
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_Detected(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Detected detectedData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, detectedData)
    {
        this.meleeMonster = meleeMonster;
    }

    public override void Update()
    {
        base.Update();
        if (isDetectedOver)
        {
            if (sense.MeleeAttack())
            {
                stateMachine.ChangeState(meleeMonster.meleeAttack);
            }
            else if (sense.MinDetected())
            {
                stateMachine.ChangeState(meleeMonster.charge);
            }
            else if (sense.MaxDetected())
            {
                stateMachine.ChangeState(meleeMonster.ability1);
            }
            else
            {
                stateMachine.ChangeState(meleeMonster.findPlayer);
            }
        }
    }
}