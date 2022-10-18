using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Dodge : E_Dodge
{
    public E_S_Dodge(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dodge dodgeData) : base(stateMachine, entity, anmName, dodgeData)
    {
    }

    public override void Update()
    {
        base.Update();
        if (isDodgeOver)
        {
            stateMachine.ChangeState(state.remoteAttack);
        }
    }
}