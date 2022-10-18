using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Dead : E_Dead
{
    public E_S_Dead(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dead deadData) : base(stateMachine, entity, anmName, deadData)
    {
    }

    public override void Update()
    {
        base.Update();
        if (isdeadOver)
        {
            stateMachine.ChangeState(state.move);
        }
    }
}