using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Dead : P_State
{
    public P_Dead(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (Application.platform == RuntimePlatform.Android)
        {
            player.SetCanvasBtnState(false);
        }
        player.SetDeadTimer(true);
        player.SetDestory();
        EffectBox.Instance.Chunk(player.transform.position);
        EffectBox.Instance.Blood(player.transform.position);
        GameManager.Instance.Rebirth();
    }
}