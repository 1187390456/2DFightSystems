using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Idle : E_State
{
    public bool isIdleTimeOver;//空闲时间是否结束
    public float idleTime = 2f;//空闲时间
    protected D_E_Idle idleData;
    public E_Idle(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Idle idleData) : base(stateMachine, entity, animName)
    {
        this.idleData = idleData;
    }

    public override void Enter()
    {
        base.Enter();
        idleTime = idleData.idleTime;
        isIdleTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
        entity.Turn();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public override void Update()
    {
        base.Update();
        idleTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            isIdleTimeOver = true;
        }
    }
}
