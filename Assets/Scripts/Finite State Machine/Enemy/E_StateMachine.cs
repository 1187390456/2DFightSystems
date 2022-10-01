using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_StateMachine
{
    public E_State currentState; // 当前状态
    public E_State lastState; // 上一次状态

    // 初始状态
    public void Init(E_State startState)
    {
        currentState = startState;
        lastState = startState;
        currentState.Enter();
    }

    // 修改状态
    public void ChangeState(E_State newState)
    {
        currentState.Exit();
        lastState = currentState;
        currentState = newState;
        currentState.Enter();
    }
}