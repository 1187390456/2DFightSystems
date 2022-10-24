using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Freak_Dead : E_Dead
{
    protected Freak freak;

    public Freak_Dead(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dead deadData, Freak freak) : base(stateMachine, entity, anmName, deadData)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (isdeadOver)
        {
            stateMachine.ChangeState(freak.move);
        }
    }
}