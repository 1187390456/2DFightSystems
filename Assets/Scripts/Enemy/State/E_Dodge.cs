﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Dodge : E_State
{
    public D_E_Dodge dodgeData { get; private set; }
    public float endDodgeTime { get; private set; } // 结束闪避时间
    protected bool isDodgeOver; // 闪避是否结束

    public E_Dodge(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Dodge dodgeData) : base(stateMachine, entity, anmName)
    {
        this.dodgeData = dodgeData;
    }

    public override void Enter()
    {
        base.Enter();
        isDodgeOver = false;
        entity.SetVelocity(dodgeData.speed, dodgeData.angle, -entity.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        endDodgeTime = Time.time;
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (Time.time >= startTime + dodgeData.dodgeTime && entity.CheckGround())
        {
            isDodgeOver = true;
        }
    }
}