using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class E_Ability1 : E_State
{
    protected D_E_Ability1 abilityData;
    protected float currentTransparent; // 当前透明度
    protected bool isAbility1Over; // 技能1是否结束

    public E_Ability1(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Ability1 abilityData) : base(stateMachine, entity, anmName)
    {
        this.abilityData = abilityData;
    }

    public override void Enter()
    {
        base.Enter();
        isAbility1Over = false;
        currentTransparent = 1;
    }

    public override void Exit()
    {
        base.Exit();
        Color color = new Color(1f, 1f, 1f, 1);
        if (abilityData.isSpine)
        {
            entity.mpb.SetColor("_Color", color);
            entity.render.SetPropertyBlock(entity.mpb);
        }
        else
        {
            entity.render.material.color = color;
        }
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        currentTransparent -= abilityData.transparentSpace * Time.deltaTime;
        Color color = new Color(1f, 1f, 1f, currentTransparent);
        if (abilityData.isSpine)
        {
            entity.mpb.SetColor("_Color", color);
            entity.render.SetPropertyBlock(entity.mpb);
        }
        else
        {
            entity.render.material.color = color;
        }
        if (currentTransparent <= 0)
        {
            var playerPos = PlayerController.Instance.transform.position;
            var dir = PlayerController.Instance.facingDirection;
            if (dir != entity.facingDirection)
            {
                entity.Turn();
            }
            if (dir == 1)
            {
                entity.aliveGobj.transform.position = new Vector2(playerPos.x - 1.0f, playerPos.y);
            }
            else if (dir == -1)
            {
                entity.aliveGobj.transform.position = new Vector2(playerPos.x + 1.0f, playerPos.y);
            }
            isAbility1Over = true;
        }
    }
}