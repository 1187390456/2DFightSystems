using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Detected : E_Detected
{
    private WildBoar wildBoar;// 野猪

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
        if (isDetectedOver)
        {
            if (entity.CheckMinDetected())
            {
                stateMachine.ChangeState(wildBoar.charge);
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