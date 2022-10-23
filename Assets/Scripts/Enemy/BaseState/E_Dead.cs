using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class E_Dead : E_State
{
    protected D_E_Dead deadData;
    protected bool isdeadOver;
    protected float currentTransparent;

    public E_Dead(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Dead deadData) : base(stateMachine, entity, anmName)
    {
        this.deadData = deadData;
    }

    public override void Enter()
    {
        base.Enter();
        isdeadOver = false;
        entity.deading = true;
        currentTransparent = 1;
        if (!deadData.isSpine)
        {
            entity.gameObject.SetActive(false);
        }
    }

    public override void Exit()
    {
        base.Exit();
        entity.deading = false;
        currentTransparent = 1;
        entity.isUseAbility2 = false;
        entity.stats.currentHealth = 999.0f;
        entity.transform.position = GameManager.Instance.GetRandPos();
        entity.transform.position = new Vector2(entity.transform.position.x, entity.transform.position.y + 1.0f);
        entity.gameObject.SetActive(true);
        if (deadData.isSpine)
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
        if (!isdeadOver && deadData.isSpine && Time.time - startTime >= 1.833f)
        {
            currentTransparent -= deadData.transparentSpace * Time.deltaTime;
            entity.SetSpineTransparent(currentTransparent);
            if (currentTransparent <= 0)
            {
                entity.gameObject.SetActive(false);
            }
        }
        if (!isdeadOver && deadData.canRebirth && Time.time >= startTime + deadData.rebirthTime + 1.833f)
        {
            isdeadOver = true;
        }
    }
}