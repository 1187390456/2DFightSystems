using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster_FindPlayer : E_FindPlayer
{
    protected MeleeMonster meleeMonster;

    public MeleeMonster_FindPlayer(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_FindPlayer findPlayerData, MeleeMonster meleeMonster) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.meleeMonster = meleeMonster;
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
                    stateMachine.ChangeState(meleeMonster.idle);
                }
                else
                {
                    stateMachine.ChangeState(meleeMonster.charge);
                }
            }
            else
            {
                stateMachine.ChangeState(meleeMonster.move);
            }
        }
        else if (!sense.MinDetected() && sense.MaxDetected())
        {
            stateMachine.ChangeState(meleeMonster.ability1);
        }
    }
}