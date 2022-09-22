using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Charge : E_Charge
{
    private WildBoar wildBoar; // 野猪

    public WildBoar_Charge(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Charge chargeData, WildBoar wildBoar) : base(stateMachine, entity, anmName, chargeData)
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
        if (isChargeOver)
        {
            stateMachine.ChangeState(wildBoar.detected);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}