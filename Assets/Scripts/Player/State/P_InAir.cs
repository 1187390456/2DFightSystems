using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_InAir : P_State
{
    protected bool isGraceTimeing; // 是否是土狼时间

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

        CheckGraceTime();
        CheckJumpInputStop();

        if (player.GroundCondition())
        {
            stateMachine.ChangeState(player.land);
        }
        else if (player.JumpCondition())
        {
            player.UseJumpInput();
            stateMachine.ChangeState(player.jump);
        }
        else if (player.ChechWall() && player.GetXInput() == player.facingDireciton && player.rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.slide);
        }
        else
        {
            player.SetVelocityX(playerData.moveSpeed * InputManager.Instance.xInput);
            player.CheckTurn();
            player.at.SetFloat("xInput", Mathf.Abs(player.GetXInput()));
            player.at.SetFloat("yVelocity", player.rb.velocity.y);
        }
    }

    private void CheckGraceTime()
    {
        if (isGraceTimeing && Time.time >= startTime + playerData.graceTime)
        {
            player.jump.DecreaseJumpCount();
            isGraceTimeing = false;
        }
    }

    public void CheckJumpInputStop()
    {
        if (player.GetJumpInputStop())
        {
            player.UseJumpInputStop();
            player.SetVelocitY(player.rb.velocity.y * playerData.jumpAirMultiplier);
        }
    }

    public void StartGraceTime() => isGraceTimeing = true;
}