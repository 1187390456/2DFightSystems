using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Hurt : E_Hurt
{
    private WildBoar wildBoar;

    public WildBoar_Hurt(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Hurt hurtData, WildBoar wildBoar) : base(stateMachine, entity, anmName, hurtData)
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
        if (entity.IsReachCanMeleeAttack())
        {
            stateMachine.ChangeState(wildBoar.meleeAttack);
        }
        else if (isHurtOver)
        {
            if (entity.CheckMinDetected())
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