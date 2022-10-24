using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal_FindPlayer : E_FindPlayer
{
    protected MeleeNormal meleeNormal;

    public MeleeNormal_FindPlayer(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_FindPlayer findPlayerData, MeleeNormal meleeNormal) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.meleeNormal = meleeNormal;
    }

    public override void Update()
    {
        base.Update();
        if (findPlayerTimeOver)
        {
            if (isFindPlayer)
            {
                if (sense.Protect())
                {
                    stateMachine.ChangeState(meleeNormal.idle);
                }
                else
                {
                    stateMachine.ChangeState(meleeNormal.charge);
                }
            }
            else
            {
                stateMachine.ChangeState(meleeNormal.move);
            }
        }
    }
}