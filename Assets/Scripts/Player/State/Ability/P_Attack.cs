using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Attack : P_Ability
{
    public P_Attack(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("1");
    }

    public override void Exit()
    {
        base.Exit();
    }
}