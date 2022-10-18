using EpicToonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_RemoteAttack : E_Attack
{
    protected D_E_RemoteAttack remoteAttackData;

    public E_RemoteAttack(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData) : base(stateMachine, entity, anmName, attackPos)
    {
        this.remoteAttackData = remoteAttackData;
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

        if (!state.deadData.isMonster)
        {
            var bulletScript = BulletBox.Instance.GetArcherBullet(remoteAttackData.bullet, attackPos);
            bulletScript.AcceptParamas(remoteAttackData.speed, remoteAttackData.distance, remoteAttackData.gravityScale, remoteAttackData.damage);
        }
        else
        {
            if (ETFXFireProjectile.Instance)
            {
                ETFXFireProjectile.Instance.CreateEnemyProjectile(sense.remoteAttackCheck);
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }
}