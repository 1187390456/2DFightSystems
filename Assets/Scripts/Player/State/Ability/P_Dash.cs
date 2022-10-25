using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Dash : P_Ability
{
    protected bool canDash = true;
    protected bool isHolding;
    protected float lastDashTime;
    protected float angle;
    protected Vector2 dashDirection;
    protected Vector2 lastAfterImagePos;

    #region 移动端

    private void AndriodDash()
    {
        dashDirection = Vector2Int.RoundToInt(action.GetDashDirtion().normalized);
        if (dashDirection != Vector2.zero)
        {
            dashDirection.Normalize();
        }
        angle = Vector2.SignedAngle(Vector2.right, dashDirection);
        player.dashIndicator.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 45.0f);
    }

    #endregion 移动端

    #region PC端

    protected float lastRotTime;
    protected float rotSpace = 0.2f;
    protected float rotCount = 1;

    private void PcDash()
    {
        if (Time.unscaledTime >= lastRotTime + rotSpace)
        {
            if (rotCount == 8) rotCount = 0;
            lastRotTime = Time.unscaledTime;
            var dir = Quaternion.AngleAxis(45 * rotCount, Vector3.forward);
            player.dashIndicator.transform.rotation = dir;
            angle = 45.0f * rotCount + 45.0f;
            dashDirection = StaticWays.JudgeDirection(angle).normalized;
            rotCount++;
        }
    }

    #endregion PC端

    public P_Dash(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public bool CheckCanDash() => canDash && Time.time >= data.dashCoolDown + lastDashTime;

    public override void Exit()
    {
        base.Exit();
        lastDashTime = Time.time;
        canDash = true;

        if (movement.rbY > 0)
        {
            movement.SetVelocityY(movement.rbY * data.dashMultiplier);
        }

        state.jump.ResetJumpCount();
    }

    public override void Enter()
    {
        base.Enter();
        canDash = false;
        isHolding = true;
        action.UseDashInput();
        Time.timeScale = data.dashTimeScale;
        startTime = Time.unscaledTime;
        player.dashIndicator.SetActive(true);
    }

    public override void Update()
    {
        base.Update();
        if (!isExit)
        {
            if (isHolding)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    AndriodDash();
                }
                else
                {
                    PcDash();
                }

                if (action.GetDashInputStop() || Time.unscaledTime >= startTime + data.maxHoldTime)
                {
                    StartDash();
                }
            }
            else
            {
                movement.SetVelocity(data.dashSpeed, dashDirection);
                CheckShouldCreateAfterImage();
                if (Time.time >= startTime + data.dashTime)
                {
                    movement.rb.drag = 0.0f;
                    isAbilityDone = true;
                }
                if (sense.Ground())
                {
                    movement.rb.drag = 0.0f;
                    movement.SetVelocityZero();
                    isAbilityDone = true;
                }
            }
        }
    }

    private void StartDash()
    {
        isHolding = false;
        Time.timeScale = 1.0f;
        startTime = Time.time;
        movement.rb.drag = data.dashDrag;
        movement.SetVelocity(data.dashSpeed, dashDirection);
        CheckTurn();
        CheckShouldCreateAfterImage();
    }

    private void CheckTurn()
    {
        player.dashIndicator.SetActive(false);
        if (TurnCondition(StaticWays.JudgeQuadrant(angle)))
        {
            movement.SetTurn();
        }
    }

    private bool TurnCondition(int quadrant) => (player.movement.facingDireciton == 1 && (quadrant == 2 || quadrant == 3)) || (player.movement.facingDireciton == -1 && (quadrant == 1 || quadrant == 4));

    private void CheckShouldCreateAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAfterImagePos) >= data.afterImageSpace)
        {
            CreateAfterImage();
        }
    }

    private void CreateAfterImage()
    {
        ObjectPool.Instance.GetObjFormPool();
        lastAfterImagePos = player.transform.position;
    }
}