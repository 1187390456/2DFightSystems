using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_Detected : E_Detected
{
    private Monster1 monster1;

    public Monster1_Detected(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Detected detectedData, Monster1 monster1) : base(stateMachine, entity, anmName, detectedData)
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
        if (isDetectedOver)
        {
            if (entity.IsReachCanMeleeAttack())
            {
                stateMachine.ChangeState(monster1.meleeAttack);
            }
            else if (entity.CheckMinDetected())
            {
                stateMachine.ChangeState(monster1.charge);
            }
            else if (entity.CheckMaxDetected())
            {
                stateMachine.ChangeState(monster1.ability1);
            }
            else
            {
                stateMachine.ChangeState(monster1.findPlayer);
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }
}