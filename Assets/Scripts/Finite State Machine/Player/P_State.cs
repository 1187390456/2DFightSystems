using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_State
{
    protected P_StateMachine stateMachine;
    protected Player player;
    protected string anmName;
    protected D_P_Base playerData;
    protected float startTime;

    public P_State(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.anmName = anmName;
        this.playerData = playerData;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        player.at.SetBool(anmName, true);
    }

    public virtual void Exit()
    {
        player.at.SetBool(anmName, false);
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}