using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Dash : P_Ability
{
    protected bool canDash = true;
    protected float lastDashTime;
    protected bool isHolding;
    protected Vector2 dashDirection;
    protected Vector2 lastAfterImagePos;

    #region 移动端

    protected float angle;

    private void AndriodDash()
    {
        dashDirection = Vector2Int.RoundToInt(player.GetDashDirtion().normalized);
        Debug.Log(dashDirection);
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
    protected float pcAngle;

    private void PcDash()
    {
        if (Time.unscaledTime >= lastRotTime + rotSpace)
        {
            if (rotCount == 8) rotCount = 0;
            lastRotTime = Time.unscaledTime;
            pcAngle = 45 * rotCount;
            var dir = Quaternion.AngleAxis(pcAngle, Vector3.forward);
            player.dashIndicator.transform.rotation = dir;
            dashDirection = StaticWays.JudgeDirection(pcAngle + 45.0f).normalized;
            rotCount++;
        }
    }

    #endregion PC端

    public P_Dash(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public bool CheckCanDash() => canDash && Time.time >= playerData.dashCoolDown + lastDashTime;

    public override void Exit()
    {
        base.Exit();
        lastDashTime = Time.time;
        canDash = true;

        if (player.rb.velocity.y > 0)
        {
            player.SetVelocitY(player.rb.velocity.y * playerData.dashMultiplier);
        }

        player.jump.ResetJumpCount();
    }

    public override void Enter()
    {
        base.Enter();
        canDash = false;
        isHolding = true;
        player.UseDashInput();
        Time.timeScale = playerData.dashTimeScale;
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

                if (player.GetDashInputStop() || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    StartDash();
                }
            }
            else
            {
                player.SetVelocity(playerData.dashSpeed, dashDirection);
                CheckShouldCreateAfterImage();
                if (Time.time >= startTime + playerData.dashTime)
                {
                    CheckEnvironment();
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
        player.rb.drag = playerData.dashDrag;
        player.SetVelocity(playerData.dashSpeed, dashDirection);
        CheckShouldCreateAfterImage();
    }

    private void CheckEnvironment()
    {
        int quadrant;

        if (Application.platform == RuntimePlatform.Android)
        {
            quadrant = StaticWays.JudgeQuadrant(angle);
        }
        else
        {
            quadrant = StaticWays.JudgeQuadrant(pcAngle + 45.0f);
        }
        if (TurnCondition(quadrant))
        {
            player.SetTurn();
        }
        player.rb.drag = 0.0f;
        player.dashIndicator.SetActive(false);
    }

    private bool TurnCondition(int quadrant) => (player.facingDireciton == 1 && (quadrant == 2 || quadrant == 3)) || (player.facingDireciton == -1 && (quadrant == 1 || quadrant == 4));

    private void CheckShouldCreateAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAfterImagePos) >= playerData.afterImageSpace)
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