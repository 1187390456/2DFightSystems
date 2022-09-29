using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_State
{
    protected P_StateMachine stateMachine;
    protected string anmName;
    protected Player player;

    public virtual void Enter()
    {
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