using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Catch : P_Wall
{
    protected Vector2 startPos; // 记录初始位置 防止重力下坠

    public P_Catch(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startPos = player.transform.position;
    }

    public override void Update()
    {
        base.Update();

        if (!isExit)
        {
            movement.SetHoldStatic(startPos);
            if (action.GetYInput() > 0)
            {
                stateMachine.ChangeState(state.climb);
            }
            else if (action.GetYInput() < 0 || !action.GetCatchInput())
            {
                stateMachine.ChangeState(state.slide);
            }
        }
    }
}