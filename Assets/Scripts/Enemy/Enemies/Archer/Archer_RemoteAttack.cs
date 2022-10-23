using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_RemoteAttack : E_RemoteAttack
{
    protected Archer archer;

    public Archer_RemoteAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, Archer archer) : base(stateMachine, entity, anmName, attackPos, remoteAttackData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (isFinshAttack)
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
                return;
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