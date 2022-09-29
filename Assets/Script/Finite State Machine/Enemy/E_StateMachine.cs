using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_StateMachine
{
    public E_State lastState;//上一次状态
    public E_State currentState;
    //初始化状态
    public virtual void InitState(E_State startState)
    {
        currentState = startState;
        currentState.Enter();
    }
    //修改状态
    public virtual void ChangeState(E_State newState)
    {
        currentState.Exit();

        lastState = currentState;
        currentState = newState;

        currentState.Enter();
    }
}
