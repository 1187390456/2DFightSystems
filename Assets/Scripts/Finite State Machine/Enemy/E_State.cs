﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_State
{
    protected E_StateMachine stateMachine; // 状态机
    protected E_Entity entity; // 实体
    protected float startTime; // 状态开始时间
    protected string anmName; // 动画名称

    public E_State(E_StateMachine stateMachine, E_Entity entity, string anmName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.anmName = anmName;
    }

    // 进入状态
    public virtual void Enter()
    {
        startTime = Time.time;
        entity.at.SetBool(anmName, true);
    }

    // 退出状态
    public virtual void Exit()
    {
        entity.at.SetBool(anmName, false);
    }

    // 在Update更新
    public virtual void Update()
    {
    }

    // 在FixUpdate更新
    public virtual void FixUpdate()
    {
    }
}