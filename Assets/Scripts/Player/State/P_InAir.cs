using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_InAir : P_State
{
    public P_InAir(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
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
        if (player.GroundDetected())
        {
            stateMachine.ChangeState(player.land);
        }
        else if (player.GetJumpInput() && player.jump.ChechCanJump())
        {
            stateMachine.ChangeState(player.jump);
        }
        else
        {
            player.SetVelocityX(playerData.moveSpeed * InputManager.Instance.xInput);
            player.CheckTurn();
            player.at.SetFloat("xInput", Mathf.Abs(player.GetXInput()));
            player.at.SetFloat("yVelocity", player.rb.velocity.y);
        }
    }
}