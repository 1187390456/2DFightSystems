using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak_FindPlayer : E_FindPlayer
{
    protected Freak freak;

    public Freak_FindPlayer(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_FindPlayer findPlayerData, Freak freak) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.freak = freak;
    }

    public override void Update()
    {
        base.Update();
        if (findPlayerTimeOver)
        {
            if (isFindPlayer)
            {
                stateMachine.ChangeState(freak.remoteAttack);
            }
            else
            {
                stateMachine.ChangeState(freak.move);
            }
        }
        else if (!sense.MinDetected() && sense.MaxDetected())
        {
            stateMachine.ChangeState(freak.remoteAttack);
        }
    }
}