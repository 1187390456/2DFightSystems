using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Detected : E_Detected
{
    private WildBoar wildBoar;

    public WildBoar_Detected(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Detected detectedData, WildBoar wildBoar) : base(stateMachine, entity, anmName, detectedData)
    {
        this.wildBoar = wildBoar;
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
        if (entity.IsReachCanMeleeAttack())
        {
            stateMachine.ChangeState(wildBoar.meleeAttack);
        }
        else if (isDetectedOver)
        {
            if (entity.CheckMinDetected())
            {
                if (entity.IsProtect())
                {
                    entity.Turn();
                    stateMachine.ChangeState(wildBoar.move);
                }
                else
                {
                    stateMachine.ChangeState(wildBoar.charge);
                }
            }
            else
            {
                stateMachine.ChangeState(wildBoar.findPlayer);
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }
}