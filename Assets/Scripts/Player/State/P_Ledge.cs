using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Ledge : P_State
{
    protected bool isCatch;
    protected bool isClimb;
    protected bool isLedgeDone;
    protected Vector2 startPos;
    protected Vector2 endPos;
    protected Vector2 workSpace;

    public P_Ledge(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // 原理 利用角位置减去或加上 角色碰撞边界的一半 就是起始位置与结束位置
        var cornerPos = player.ComputedCornerPos();
        var boundSize = player.collider2d.bounds.size;
        workSpace.Set(boundSize.x / 2 * player.facingDireciton, boundSize.y / 2);
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
            player.at.SetBool("ledgeClimb", false);
            player.transform.position = endPos;
            stateMachine.ChangeState(player.idle);
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
        }
    }
}