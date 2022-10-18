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
            stateMachine.ChangeState(state.firstAttack);
        }
        else if (player.SecondAttackCondition())
        {
            stateMachine.ChangeState(state.secondAttack);
        }
        else if (player.DashCondition())
        {
            stateMachine.ChangeState(state.dash);
        }
        else if (player.LedgeCondition())
        {
            stateMachine.ChangeState(state.ledge);
        }
        else if (player.GroundCondition())
        {
            stateMachine.ChangeState(state.land);
        }
        else if (WallJumpCondition())
        {
            UseWallJumpGraceTime();
            stateMachine.ChangeState(state.wallJump);
        }
        else if (player.JumpCondition())
        {
            stateMachine.ChangeState(state.jump);
        }
        else if (player.CatchWallConditon())
        {
            stateMachine.ChangeState(state.catchWall);
        }
        else if (SildeCondition())
        {
            stateMachine.ChangeState(state.slide);
        }
        else
        {
            movement.SetPlayerMove(data.moveSpeed);
            movement.CheckTurn();
        }
    }

    private bool SildeCondition() => sense.Wall() && action.GetXInput() == movement.facingDireciton && movement.rbY < 0;

    private bool WallJumpCondition() => (sense.Wall() || sense.BackWall() || isWallJumpGraceTiming) && player.JumpCondition();

    public void StartGraceTime() => isGraceTiming = true;

    private void CheckGraceTime()
    {
        if (isGraceTiming && Time.time >= startTime + data.graceTime)
        {
            isGraceTiming = false;
        }
        if (sense.Wall())
        {
            state.jump.ResetJumpCount();
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
        if (isWallJumpGraceTiming && Time.time >= startWallJumpGraceTime + data.wallJumpGraceTime)
        {
            isWallJumpGraceTiming = false;
        }
    }

    private void UseWallJumpGraceTime() => isWallJumpGraceTiming = false;

    public void CheckJumpInputStop()
    {
        if (action.GetJumpInputStop())
        {
            action.UseJumpInputStop();
            movement.SetVelocityY(movement.rbY * data.jumpAirMultiplier);
        }
    }
}