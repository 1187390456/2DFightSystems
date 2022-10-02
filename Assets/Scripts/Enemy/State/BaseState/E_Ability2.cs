using UnityEngine;

public class E_Ability2 : E_RemoteAttack
{
    protected D_E_Ability2 ability2Data;
    public bool isAbility2Over;

    public E_Ability2(E_StateMachine stateMachine, Enemy entity, string anmName, Transform attackPos, D_E_RemoteAttack remoteAttackData, D_E_Ability2 ability2Data) : base(stateMachine, entity, anmName, attackPos, remoteAttackData)
    {
        this.ability2Data = ability2Data;
    }

    public override void Enter()
    {
        base.Enter();
        isAbility2Over = false;
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
        if (PlayerController.Instance.transform.position.x < entity.aliveGobj.transform.position.x && entity.facingDirection == 1)
        {
            entity.Turn();
        }
        else if (PlayerController.Instance.transform.position.x > entity.aliveGobj.transform.position.x && entity.facingDirection == -1)
        {
            entity.Turn();
        }

        if (Time.time >= startTime + ability2Data.ability2Time)
        {
            isAbility2Over = true;
        }
    }
}