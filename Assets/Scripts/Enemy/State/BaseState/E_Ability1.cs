﻿using UnityEngine;
using Color = UnityEngine.Color;

public class E_Ability1 : E_State
{
    protected D_E_Ability1 abilityData;
    protected float currentTransparent; // 当前透明度
    protected bool isAbility1Over; // 技能1是否结束

    public E_Ability1(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_Ability1 abilityData) : base(stateMachine, entity, anmName)
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
        currentTransparent = 1;
        Color color = new Color(1f, 1f, 1f, 1);
        JudgeIsSpineFixTransparent(color);
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
        JudgeIsSpineFixTransparent(color);
        CheckIsDisappear();
    }

    // 判断是否是骨骼动画 修复透明度
    protected void JudgeIsSpineFixTransparent(Color color)
    {
        if (abilityData.isSpine)
        {
            entity.SetSpineTransparent(currentTransparent);
        }
        else
        {
            entity.render.material.color = color;
        }
    }

    // 检测是否完全消失
    protected void CheckIsDisappear()
    {
        if (currentTransparent <= 0 && Player.Instance)
        {
            var playerPos = Player.Instance.transform.position;
            var dir = Player.Instance.facingDireciton;
            if (dir != entity.facingDirection)
            {
                entity.Turn();
            }
            if (dir == 1)
            {
                var pos = new Vector2(playerPos.x - 1.0f, playerPos.y);
                entity.aliveGobj.transform.position = pos;
            }
            else if (dir == -1)
            {
                var pos = new Vector2(playerPos.x + 1.0f, playerPos.y);
                entity.aliveGobj.transform.position = pos;
            }
            isAbility1Over = true;
        }
    }
}