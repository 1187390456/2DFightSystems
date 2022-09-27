using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_FindPlayer : E_FindPlayer
{
    protected WildBoar wildBoar;

    public WildBoar_FindPlayer(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_FindPlayer findPlayerData, WildBoar wildBoar) : base(stateMachine, entity, anmName, findPlayerData)
    {
        this.wildBoar = wildBoar;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (findPlayerTimeOver)
        {
            if (isFindPlayer)
            {
                if (entity.IsProtect())
                {
                    stateMachine.ChangeState(wildBoar.move);
                }
                else
                {
                    stateMachine.ChangeState(wildBoar.charge);
                }
            }
            else
            {
                stateMachine.ChangeState(wildBoar.move);
            }
        }
    }
}