using UnityEngine;

public class P_InAir : P_State
{
    protected bool isGraceTiming;

    protected bool isWallJumpGraceTiming;

    protected float startWallJumpGraceTime;

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
        CheckWallJumpGraceTime();
        CheckJumpInputStop();

        if (player.FirstAttackCondition())
        {
            stateMachine.ChangeState(player.firstAttack);
        }
        else if (player.SecondAttackCondition())
        {
            stateMachine.ChangeState(player.secondAttack);
        }
        else if (player.DashCondition())
        {
            stateMachine.ChangeState(player.dash);
        }
        else if (player.LedgeCondition())
        {
            stateMachine.ChangeState(player.ledge);
        }
        else if (player.GroundCondition())
        {
            stateMachine.ChangeState(player.land);
        }
        else if (WallJumpCondition())
        {
            UseWallJumpGraceTime();
            stateMachine.ChangeState(player.wallJump);
        }
        else if (player.JumpCondition())
        {
            stateMachine.ChangeState(player.jump);
        }
        else if (player.CatchWallConditon())
        {
            stateMachine.ChangeState(player.catchWall);
        }
        else if (SildeCondition())
        {
            stateMachine.ChangeState(player.slide);
        }
        else
        {
            player.SetPlayerMove(playerData.moveSpeed);
            player.CheckTurn();
        }
    }

    private bool SildeCondition() => player.ChechWall() && player.GetXInput() == player.facingDireciton && player.rb.velocity.y < 0;

    private bool WallJumpCondition() => (player.ChechWall() || player.CheckBackWall() || isWallJumpGraceTiming) && player.JumpCondition();

    public void StartGraceTime() => isGraceTiming = true;

    private void CheckGraceTime()
    {
        if (isGraceTiming && Time.time >= startTime + playerData.graceTime)
        {
            isGraceTiming = false;
        }
        if (player.ChechWall())
        {
            player.jump.ResetJumpCount();
            StartWallJumpGraceTime();
        }
    }

    public void StartWallJumpGraceTime()
    {
        isWallJumpGraceTiming = true;
        startWallJumpGraceTime = Time.time;
    }

    private void CheckWallJumpGraceTime()
    {
        if (isWallJumpGraceTiming && Time.time >= startWallJumpGraceTime + playerData.wallJumpGraceTime)
        {
            isWallJumpGraceTiming = false;
        }
    }

    private void UseWallJumpGraceTime() => isWallJumpGraceTiming = false;

    public void CheckJumpInputStop()
    {
        if (player.GetJumpInputStop())
        {
            player.UseJumpInputStop();
            player.SetVelocitY(player.rb.velocity.y * playerData.jumpAirMultiplier);
        }
    }
}