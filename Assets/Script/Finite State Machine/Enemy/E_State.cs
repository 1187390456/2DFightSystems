using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_State
{
    protected E_StateMachine stateMachine;//状态机
    protected E_Entity entity;//实体
    protected string animName;//动画名称

    public E_State(E_StateMachine stateMachine, E_Entity entity, string animName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animName = animName;
    }
    public virtual void Enter()
    {
        entity.anim.SetBool(animName, true);
    }
    public virtual void Exit()
    {
        entity.anim.SetBool(animName, false);
    }
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }
}
