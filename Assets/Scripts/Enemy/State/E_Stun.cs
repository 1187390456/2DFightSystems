using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Stun : E_State
{
    protected D_E_Stun stunData; // 眩晕数据
    protected bool isStunOver; // 眩晕是否结束

    public E_Stun(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Stun stunData) : base(stateMachine, entity, anmName)
    {
        this.stunData = stunData;
    }

    public override void Enter()
    {
        base.Enter();
        isStunOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (Time.time >= startTime + stunData.stunTime)
        {
            isStunOver = true;
            entity.isStuning = false;
            entity.currentStunCount = entity.entityData.stunCount;
        }
    }
}