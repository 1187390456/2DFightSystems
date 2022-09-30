using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Detected : E_Detected
{
    private Monster Monster;

    public Monster_Detected(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Detected detectedData, Monster Monster) : base(stateMachine, entity, anmName, detectedData)
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
        if (isDetectedOver)
        {
            if (entity.IsReachCanMeleeAttack())
            {
                stateMachine.ChangeState(Monster.meleeAttack);
            }
            else if (entity.CheckMinDetected())
            {
                stateMachine.ChangeState(Monster.charge);
            }
            else if (entity.CheckMaxDetected())
            {
                stateMachine.ChangeState(Monster.ability1);
            }
            else
            {
                stateMachine.ChangeState(Monster.findPlayer);
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }
}