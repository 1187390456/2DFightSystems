using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_Dead : E_Dead
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_Dead(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dead deadData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, deadData)
    {
        this.meleeNormal = meleeNormal;
    }

    public override void Update()
    {
        base.Update();
        if (isdeadOver)
        {
            stateMachine.ChangeState(meleeNormal.move);
        }
    }
}