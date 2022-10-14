using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Ledge : P_State
{
    protected bool isCatch;
    protected bool isClimb;
    protected bool isLedgeDone;
    protected bool isHeadTouch;
    protected float offset = 0.015f;
    protected float fixOffset = 0.2215f; // 修复结束位置的高度 防止过度到空中
    protected Vector2 cornerPos;
    protected Vector2 startPos;
    protected Vector2 endPos;
    protected Vector2 workSpace;

    public P_Ledge(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        cornerPos = player.ComputedCornerPos();
        CheckWillTouchHead();
        workSpace.Set(player.normalColliderSize.x / 2 * player.facingDireciton, player.normalColliderSize.y / 2 + fixOffset);
        startPos = cornerPos - workSpace;
        endPos = cornerPos + workSpace;
        isLedgeDone = false;
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
        isLedgeDone = true;
    }

    public override void StartAnimation()
    {
        base.StartAnimation();
        isCatch = true;
    }

    public override void Update()
    {
        base.Update();
        player.SetHoldStatic(startPos);
        if (isLedgeDone)
        {
            isClimb = false;
            isCatch = false;
            player.transform.position = endPos;
            player.at.SetBool("ledgeClimb", false);

            if (isHeadTouch)
            {
                stateMachine.ChangeState(player.crouchIdle);
            }
            else
            {
                stateMachine.ChangeState(player.idle);
            }
        }
        else if (isCatch && !isClimb)
        {
            if (player.GetXInput() == player.facingDireciton)
            {
                isClimb = true;
                player.at.SetBool("ledgeClimb", true);
            }
            else if (player.GetYInput() == -1)
            {
                stateMachine.ChangeState(player.inAir);
            }
            else if (player.JumpCondition())
            {
                stateMachine.ChangeState(player.wallJump);
            }
        }
    }

    private void CheckWillTouchHead()
    {
        isHeadTouch = Physics2D.Raycast(cornerPos + Vector2.up * offset + Vector2.right * player.facingDireciton * offset, Vector2.up, player.normalColliderSize.y, LayerMask.GetMask("Ground"));
        player.at.SetBool("isHeadTouch", isHeadTouch);
    }
}