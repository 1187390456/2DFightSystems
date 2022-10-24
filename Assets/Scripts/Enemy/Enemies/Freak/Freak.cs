using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freak : Enemy
{
    [Header("技能2数据")] public D_E_Ability2 ability2Data;
    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("受伤数据")] public D_E_Hurt hurtData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("远程攻击数据")] public D_E_RemoteAttack remoteAttackData;
    [Header("眩晕数据")] public D_E_Stun stunData;

    public Freak_Ability2 ability2;
    public Freak_Dead dead;
    public Freak_Detected detected;
    public Freak_FindPlayer findPlayer;
    public Freak_Hurt hurt;
    public Freak_Idle idle;
    public Freak_Move move;
    public Freak_RemoteAttack remoteAttack;
    public Freak_Stun stun;

    private float currentHealth => stats.currentHealth;
    private int currentStunCount => stats.currentStunCount;

    public override void Start()
    {
        base.Start();

        ability2 = new Freak_Ability2(stateMachine, this, "ability2", sense.remoteAttackCheck, remoteAttackData, ability2Data, this);
        dead = new Freak_Dead(stateMachine, this, "dead", deadData, this);
        detected = new Freak_Detected(stateMachine, this, "detected", detectedData, this);

        findPlayer = new Freak_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        hurt = new Freak_Hurt(stateMachine, this, "hurt", hurtData, this);
        idle = new Freak_Idle(stateMachine, this, "idle", idleData, this);

        move = new Freak_Move(stateMachine, this, "move", moveData, this);
        stun = new Freak_Stun(stateMachine, this, "stun", stunData, this);
        remoteAttack = new Freak_RemoteAttack(stateMachine, this, "remoteAttack", sense.remoteAttackCheck, remoteAttackData, this);
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

    private bool HurtCondition() => stateMachine.currentState != stun && stateMachine.currentState != hurt;

    private bool StunConditon() => currentStunCount > 0 && stateMachine.currentState != stun;
}