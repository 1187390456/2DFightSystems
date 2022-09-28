using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class E_Dead : E_State
{
    protected D_E_Dead deadData;
    protected bool isdeadOver; // 死亡时间是否结束

    public E_Dead(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Dead deadData) : base(stateMachine, entity, anmName)
    {
        this.deadData = deadData;
    }

    public override void Enter()
    {
        base.Enter();
        isdeadOver = false;
        EffectBox.Instance.Chunk(entity.aliveGobj.transform.position);
        EffectBox.Instance.Blood(entity.aliveGobj.transform.position);
        entity.aliveGobj.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
        entity.isDead = false;
        entity.currentHealth = entity.entityData.maxHealth;
        entity.aliveGobj.transform.position = GameManager.Instance.GetRandPos();
        entity.aliveGobj.SetActive(true);
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (deadData.canRebirth && Time.time >= startTime + deadData.rebirthTime)
        {
            isdeadOver = true;
        }
    }
}