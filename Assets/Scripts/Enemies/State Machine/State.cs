using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;//状态机
    protected Entity entity;//实体

    protected float startTime;//开始时间

    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    //进入
    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
    }
    //退出
    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }
    //逻辑更新
    public virtual void LogicUpdate()
    {

    }
    //物理更新
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    //检查
    public virtual void DoChecks()
    {

    }
}
