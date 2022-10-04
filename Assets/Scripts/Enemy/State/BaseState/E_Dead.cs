using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class E_Dead : E_State
{
    protected D_E_Dead deadData;
    protected bool isdeadOver; // 死亡时间是否结束

    protected float currentTransparent; // 当前透明度

    protected int count; // 播放第几个死亡特效

    public E_Dead(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dead deadData) : base(stateMachine, entity, anmName)
    {
        this.deadData = deadData;
    }

    public override void Enter()
    {
        base.Enter();
        isdeadOver = false;
        currentTransparent = 1;
        if (!deadData.isMonster)
        {
            entity.aliveGobj.SetActive(false);
            for (int i = 0; i < deadData.effectListRes.Count; i++)
            {
                EffectBox.Instance.CreateEffect(deadData.effectListRes[i], entity.aliveGobj.transform.position, entity.aliveGobj.transform.rotation);
            }
        }
        else
        {
            GetRandomEffect();
        }
    }

    public override void Exit()
    {
        base.Exit();
        currentTransparent = 1;
        entity.isDead = false;
        entity.isUseAbility2 = false;
        entity.currentHealth = entity.entityData.maxHealth;
        // entity.aliveGobj.transform.position = GameManager.Instance.GetRandPos();
        entity.aliveGobj.transform.position = new Vector2(entity.aliveGobj.transform.position.x, entity.aliveGobj.transform.position.y + 1.0f);
        entity.aliveGobj.SetActive(true);
        if (deadData.isMonster)
        {
            entity.SetSpineTransparent(currentTransparent);
        }
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        // 死后逐渐消失
        if (!isdeadOver && deadData.isMonster && Time.time - startTime >= 1.833f)
        {
            currentTransparent -= deadData.transparentSpace * Time.deltaTime;
            entity.SetSpineTransparent(currentTransparent);
            if (currentTransparent <= 0)
            {
                entity.aliveGobj.SetActive(false);
            }
        }

        if (!isdeadOver && deadData.canRebirth && Time.time >= startTime + deadData.rebirthTime + 1.833f)
        {
            isdeadOver = true;
        }
    }

    // 随机获取一个死亡特效索引
    private void GetRandomEffect()
    {
        var index = Random.Range(0, deadData.effectListRes.Count);
        var pos = new Vector2(entity.aliveGobj.transform.position.x + deadData.effectOffset.x, entity.aliveGobj.transform.position.y + deadData.effectOffset.y);
        EffectBox.Instance.CreateEffect(deadData.effectListRes[index], pos, entity.aliveGobj.transform.rotation);
    }
}