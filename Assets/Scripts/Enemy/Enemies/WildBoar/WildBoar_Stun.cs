using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Stun : E_Stun
{
    protected WildBoar wildBoar;

    public WildBoar_Stun(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Stun stunData, WildBoar wildBoar) : base(stateMachine, entity, anmName, stunData)
    {
        this.wildBoar = wildBoar;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (isStunOver)
        {
            if (entity.IsReachCanMeleeAttack())
            {
                stateMachine.ChangeState(wildBoar.meleeAttack);
            }
            else if (entity.CheckMinDetected())
            {
                stateMachine.ChangeState(wildBoar.charge);
            }
            else
            {
                stateMachine.ChangeState(wildBoar.findPlayer);
            }
        }
    }
}