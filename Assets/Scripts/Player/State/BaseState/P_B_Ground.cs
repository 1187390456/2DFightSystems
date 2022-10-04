using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_B_Ground : P_State
{
    protected float inputX;
    protected float inputY;

    public P_B_Ground(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        inputX = player.inputManager.inputX;
        inputY = player.inputManager.inputY;
    }
}