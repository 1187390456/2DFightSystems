using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_State
{
    protected Movement movement;
    protected EnemyCollisionSenses sense;
    protected EnemyState state;

    protected E_StateMachine stateMachine; // 状态机
    protected Enemy entity; // 实体
    protected string anmName; // 动画名称
    public float startTime { get; private set; }// 状态开始时间

    public E_State(E_StateMachine stateMachine, Enemy entity, string anmName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.anmName = anmName;

        movement = entity.movement;
        sense = entity.sense;
        state = entity.state;
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