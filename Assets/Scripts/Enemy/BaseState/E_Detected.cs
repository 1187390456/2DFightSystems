using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Detected : E_State
{
    protected D_E_Detected detectedData; // 警备数据
    protected bool isDetectedOver; // 警备是否结束

    public E_Detected(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Detected detectedData) : base(stateMachine, entity, anmName)
    {
        this.detectedData = detectedData;
    }

    public override void Enter()
    {
        base.Enter();
        isDetectedOver = false;
        entity.SetVelocityX(0.0f);
    }

    public override void Exit()
    {
        base.Exit();
        // 受伤即刻进入警报
        entity.isHurting = false;
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (!isDetectedOver && Time.time >= startTime + detectedData.detectedTime)
        {
            isDetectedOver = true;
        }
    }
}