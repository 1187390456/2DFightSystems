using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Detected : E_Detected
{
    private Archer archer;

    public Archer_Detected(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Detected detectedData, Archer archer) : base(stateMachine, entity, anmName, detectedData)
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
        if (isDetectedOver)
        {
            if (entity.IsReachCanMeleeAttack())
            {
                if (archer.CheckCanDodge())
                {
                    stateMachine.ChangeState(archer.dodge);
                }
                else
                {
                    stateMachine.ChangeState(archer.meleeAttack);
                }
            }
            else if (entity.CheckMinDetected())
            {
                stateMachine.ChangeState(archer.ability1);
            }
            else if (entity.CheckMaxDetected())
            {
                stateMachine.ChangeState(archer.remoteAttack);
            }
            else if (!entity.CheckMaxDetected())
            {
                stateMachine.ChangeState(archer.findPlayer);
            }
        }
    }
}