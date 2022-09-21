using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Idle : E_State
{
    protected D_E_Idle idleData; // 空闲数据
    protected float idleTime; // 空闲时间
    protected bool isIdleTimeOver; // 空闲时间是否结束
    protected bool canTurn; // 是否能够转身

    public E_Idle(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Idle idleData) : base(stateMachine, entity, anmName)
    {
        this.idleData = idleData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0.0f);
        SetRandomIdleTime();
        canTurn = true;
        isIdleTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
        if (canTurn)
        {
            entity.Turn();
        }
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (!isIdleTimeOver && Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    // 设置随机空闲时间
    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(idleData.minIdleTime, idleData.maxIdleTime);
    }

    // 设置是否能转身
    private void SetCanTurn(bool value)
    {
        canTurn = value;
    }
}