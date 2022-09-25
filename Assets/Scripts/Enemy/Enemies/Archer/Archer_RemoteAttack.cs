using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_RemoteAttack : E_RemoteAttack
{
    public Archer archer;

    public Archer_RemoteAttack(E_StateMachine stateMachine, E_Entity entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, Archer archer) : base(stateMachine, entity, anmName, attackPos, remoteAttackData)
    {
        this.archer = archer;
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
    }
}