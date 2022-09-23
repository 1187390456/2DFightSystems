using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_MeleeAttack : E_MeleeAttack
{
    protected WildBoar wildBoar; // 野猪

    public WildBoar_MeleeAttack(E_StateMachine stateMachine, E_Entity entity, string anmName, Transform attackPos, D_E_MeleeAttack meleeAttackData, WildBoar wildBoar) : base(stateMachine, entity, anmName, attackPos, meleeAttackData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void StartAttack()
    {
        base.StartAttack();
    }

    public override void Update()
    {
        base.Update();
        if (isFinshAttack)
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