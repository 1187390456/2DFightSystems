using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Hurt : E_Hurt
{
    protected Archer archer;

    public Archer_Hurt(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Hurt hurtData, Archer archer) : base(stateMachine, entity, anmName, hurtData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (sense.MeleeAttack())
        {
            if (archer.DodgeCondition())
            {
                stateMachine.ChangeState(archer.dodge);
            }
            else
            {
                stateMachine.ChangeState(archer.meleeAttack);
            }
        }
        else if (isHurtOver)
        {
            if (sense.MinDetected())
            {
                stateMachine.ChangeState(archer.remoteAttack);
            }
            else if (sense.MaxDetected())
            {
                stateMachine.ChangeState(archer.ability1);
            }
            else
            {
                stateMachine.ChangeState(archer.findPlayer);
            }
        }
    }
}