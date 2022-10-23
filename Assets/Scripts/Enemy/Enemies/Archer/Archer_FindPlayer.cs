using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_FindPlayer : E_FindPlayer
{
    protected Archer archer;

    public Archer_FindPlayer(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_FindPlayer findPlayerData, Archer archer) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.archer = archer;
    }

    public override void Update()
    {
        base.Update();
        if (findPlayerTimeOver)
        {
            if (isFindPlayer)
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
                else
                {
                    stateMachine.ChangeState(archer.remoteAttack);
                }
            }
            else
            {
                stateMachine.ChangeState(archer.move);
            }
        }
        else if (!sense.MinDetected() && sense.MaxDetected())
        {
            stateMachine.ChangeState(archer.ability1);
        }
    }
}