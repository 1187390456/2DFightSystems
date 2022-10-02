using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Charge : E_State
{
    protected D_E_Charge chargeData; // 冲锋数据
    protected bool isChargeOver; // 冲锋是否结束

    public E_Charge(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Charge chargeData) : base(stateMachine, entity, anmName)
    {
        this.chargeData = chargeData;
    }

    public override void Enter()
    {
        base.Enter();
        isChargeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
        entity.SetVelocityX(chargeData.chargeSpeed);
    }

    public override void Update()
    {
        base.Update();
        if (Time.time >= startTime + chargeData.chargeTime)
        {
            isChargeOver = true;
        }
    }
}