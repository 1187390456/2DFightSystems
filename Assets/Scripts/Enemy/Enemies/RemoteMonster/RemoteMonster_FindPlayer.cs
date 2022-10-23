using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster_FindPlayer : E_FindPlayer
{
    protected RemoteMonster remoteMonster;

    public RemoteMonster_FindPlayer(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_FindPlayer findPlayerData, RemoteMonster remoteMonster) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.remoteMonster = remoteMonster;
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
                    stateMachine.ChangeState(remoteMonster.meleeAttack);
                }
                else
                {
                    stateMachine.ChangeState(remoteMonster.remoteAttack);
                }
            }
            else
            {
                stateMachine.ChangeState(remoteMonster.move);
            }
        }
        else if (!sense.MinDetected() && sense.MaxDetected())
        {
            stateMachine.ChangeState(remoteMonster.remoteAttack);
        }
    }
}