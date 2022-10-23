using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : Enemy
{
    [Header("技能1数据")] public D_E_Ability1 ability1Data;
    [Header("追击数据")] public D_E_Charge chargeData;
    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("受伤数据")] public D_E_Hurt hurtData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("眩晕数据")] public D_E_Stun stunData;

    public MeleeMonster_Ability1 ability1;
    public MeleeMonster_Charge charge;
    public MeleeMonster_Dead dead;
    public MeleeMonster_Detected detected;
    public MeleeMonster_FindPlayer findPlayer;
    public MeleeMonster_Hurt hurt;
    public MeleeMonster_Idle idle;
    public MeleeMonster_MeleeAttack meleeAttack;
    public MeleeMonster_Move move;
    public MeleeMonster_Stun stun;

    private float currentHealth => stats.currentHealth;
    private int currentStunCount => stats.currentStunCount;

    public override void Start()
    {
        base.Start();

        ability1 = new MeleeMonster_Ability1(stateMachine, this, "ability1", ability1Data, this);
        charge = new MeleeMonster_Charge(stateMachine, this, "charge", chargeData, this);
        dead = new MeleeMonster_Dead(stateMachine, this, "dead", deadData, this);
        detected = new MeleeMonster_Detected(stateMachine, this, "detected", detectedData, this);
        findPlayer = new MeleeMonster_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        hurt = new MeleeMonster_Hurt(stateMachine, this, "hurt", hurtData, this);
        idle = new MeleeMonster_Idle(stateMachine, this, "idle", idleData, this);
        meleeAttack = new MeleeMonster_MeleeAttack(stateMachine, this, "meleeAttack", sense.meleeAttackCheck, meleeAttackData, this);
        move = new MeleeMonster_Move(stateMachine, this, "move", moveData, this);
        stun = new MeleeMonster_Stun(stateMachine, this, "stun", stunData, this);
        stateMachine.Init(move);
    }

    public void DecreaseHealthCallBack()
    {
        if (stateMachine.currentState == dead) return;
        if (currentHealth > 0)
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
        else
        {
            stats.ClearHealth();
            stateMachine.ChangeState(dead);
        }
    }

    private bool HurtCondition() => stateMachine.currentState != stun && stateMachine.currentState != hurt;

    private bool StunConditon() => currentStunCount > 0 && stateMachine.currentState != stun;
}