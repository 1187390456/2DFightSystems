using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_StateMachine
{
    public P_State currenState { get; private set; }

    public void Init(P_State startState)
    {
        currenState = startState;
        currenState.Enter();
    }

    public void ChangeState(P_State newState)
    {
    }
}