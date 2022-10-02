using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class E_Hurt : E_State
{
    protected D_E_Hurt hurtData; // 受伤数据
    protected bool isHurtOver; // 受伤状态是否结束

    public E_Hurt(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Hurt hurtData) : base(stateMachine, entity, anmName)
    {
        this.hurtData = hurtData;
    }

    public override void Enter()
    {
        base.Enter();
        isHurtOver = false;
        entity.SetVelocity(hurtData.knockbackSpeed, hurtData.knockbackAngle, entity.knockbackDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.isHurting = false;
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (!isHurtOver && Time.time >= startTime + hurtData.hurtTime)
        {
            isHurtOver = true;
        }
    }
}