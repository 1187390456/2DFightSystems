using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteMonster : Enemy
{
    [Header("技能2数据")] public D_E_Ability2 ability2Data;
    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("受伤数据")] public D_E_Hurt hurtData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("远程攻击数据")] public D_E_RemoteAttack remoteAttackData;
    [Header("眩晕数据")] public D_E_Stun stunData;

    public RemoteMonster_Ability2 ability2;
    public RemoteMonster_Dead dead;
    public RemoteMonster_Detected detected;
    public RemoteMonster_FindPlayer findPlayer;
    public RemoteMonster_Hurt hurt;
    public RemoteMonster_Idle idle;
    public RemoteMonster_MeleeAttack meleeAttack;
    public RemoteMonster_Move move;
    public RemoteMonster_RemoteAttack remoteAttack;
    public RemoteMonster_Stun stun;

    private float currentHealth => stats.currentHealth;
    private int currentStunCount => stats.currentStunCount;

    public override void Start()
    {
        base.Start();

        ability2 = new RemoteMonster_Ability2(stateMachine, this, "ability2", sense.remoteAttackCheck, remoteAttackData, ability2Data, this);
        dead = new RemoteMonster_Dead(stateMachine, this, "dead", deadData, this);
        detected = new RemoteMonster_Detected(stateMachine, this, "detected", detectedData, this);
        findPlayer = new RemoteMonster_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        hurt = new RemoteMonster_Hurt(stateMachine, this, "hurt", hurtData, this);
        idle = new RemoteMonster_Idle(stateMachine, this, "idle", idleData, this);
        meleeAttack = new RemoteMonster_MeleeAttack(stateMachine, this, "meleeAttack", sense.meleeAttackCheck, meleeAttackData, this);
        move = new RemoteMonster_Move(stateMachine, this, "move", moveData, this);
        stun = new RemoteMonster_Stun(stateMachine, this, "stun", stunData, this);
        remoteAttack = new RemoteMonster_RemoteAttack(stateMachine, this, "remoteAttack", sense.remoteAttackCheck, remoteAttackData, this);
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