using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_MeleeAttack : E_MeleeAttack
{
    protected Archer archer;

    public Archer_MeleeAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData, Archer archer) : base(stateMachine, entity, anmName, attackPos, meleeAttackData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (isFinshAttack)
        {
            if (archer.DodgeCondition())
            {
                stateMachine.ChangeState(archer.dodge);
            }
            else if (sense.MeleeAttack())
            {
                return;
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