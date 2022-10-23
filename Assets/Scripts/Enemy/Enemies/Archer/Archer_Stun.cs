using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Stun : E_Stun
{
    protected Archer archer;

    public Archer_Stun(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Stun stunData, Archer archer) : base(stateMachine, entity, anmName, stunData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (isStunOver)
        {
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
            else if (sense.MinDetected())
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