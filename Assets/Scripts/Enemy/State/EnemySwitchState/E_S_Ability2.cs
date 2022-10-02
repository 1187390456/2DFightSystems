using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_S_Ability2 : E_Ability2
{
    public E_S_Ability2(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, D_E_Ability2 ability2Data) : base(stateMachine, entity, anmName, attackPos, remoteAttackData, ability2Data)
    {
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
        if (isAbility2Over)
        {
            stateMachine.ChangeState(entity.move);
        }
    }
}