using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [Header("技能1数据")] public D_E_Ability1 ability1Data;
    [Header("技能2数据")] public D_E_Ability2 ability2Data;
    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("闪避数据")] public D_E_Dodge dodgeData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("受伤数据")] public D_E_Hurt hurtData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("远程攻击数据")] public D_E_RemoteAttack remoteAttackData;
    [Header("眩晕数据")] public D_E_Stun stunData;

    public Archer_Ability1 ability1;
    public Archer_Ability2 ability2;
    public Archer_Dead dead;
    public Archer_Detected detected;
    public Archer_Dodge dodge;
    public Archer_FindPlayer findPlayer;
    public Archer_Hurt hurt;
    public Archer_Idle idle;
    public Archer_MeleeAttack meleeAttack;
    public Archer_Move move;
    public Archer_RemoteAttack remoteAttack;
    public Archer_Stun stun;

    private float currentHealth => stats.currentHealth;
    private int currentStunCount => stats.currentStunCount;

    public override void Start()
    {
        base.Start();

        ability1 = new Archer_Ability1(stateMachine, this, "ability1", ability1Data, this);
        ability2 = new Archer_Ability2(stateMachine, this, "ability2", sense.remoteAttackCheck, remoteAttackData, ability2Data, this);
        dead = new Archer_Dead(stateMachine, this, "dead", deadData, this);
        detected = new Archer_Detected(stateMachine, this, "detected", detectedData, this);
        dodge = new Archer_Dodge(stateMachine, this, "dodge", dodgeData, this);
        findPlayer = new Archer_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        hurt = new Archer_Hurt(stateMachine, this, "hurt", hurtData, this);
        idle = new Archer_Idle(stateMachine, this, "idle", idleData, this);
        meleeAttack = new Archer_MeleeAttack(stateMachine, this, "meleeAttack", sense.meleeAttackCheck, meleeAttackData, this);
        move = new Archer_Move(stateMachine, this, "move", moveData, this);
        stun = new Archer_Stun(stateMachine, this, "stun", stunData, this);
        remoteAttack = new Archer_RemoteAttack(stateMachine, this, "remoteAttack", sense.remoteAttackCheck, remoteAttackData, this);
        stateMachine.Init(move);
    }

    public void DecreaseHealthCallBack()
    {
        if (stateMachine.currentState == dead || stateMachine.currentState == ability2) return;
        if (currentHealth > 0)
        {
            if (currentHealth <= 50.0f && !isUseAbility2)
            {
                isUseAbility2 = true;
                stateMachine.ChangeState(ability2);
            }
            else
            {
                if (StunConditon())
                {
                    stats.DecreaseStunCount();
                    if (currentStunCount <= 0)
                    {
                        stats.ResetStunCount();
                        stateMachine.ChangeState(stun);
                    }
                }

                if (HurtCondition())
                {
                    stateMachine.ChangeState(hurt);
                }
            }
        }
        else
        {
            stats.ClearHealth();
            stateMachine.ChangeState(dead);
        }
    }

    public bool DodgeCondition() => Time.time >= dodge.endDodgeTime + dodge.dodgeData.cooldown;

    private bool HurtCondition() => stateMachine.currentState != stun && stateMachine.currentState != hurt;

    private bool StunConditon() => currentStunCount > 0 && stateMachine.currentState != stun;
}