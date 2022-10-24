using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNormal : Enemy
{
    [Header("追击数据")] public D_E_Charge chargeData;
    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("受伤数据")] public D_E_Hurt hurtData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("眩晕数据")] public D_E_Stun stunData;

    public MeleeNormal_Charge charge;
    public MeleeNormal_Dead dead;
    public MeleeNormal_Detected detected;
    public MeleeNormal_FindPlayer findPlayer;
    public MeleeNormal_Hurt hurt;
    public MeleeNormal_Idle idle;
    public MeleeNormal_MeleeAttack meleeAttack;
    public MeleeNormal_Move move;
    public MeleeNormal_Stun stun;

    private float currentHealth => stats.currentHealth;
    private int currentStunCount => stats.currentStunCount;

    public override void Start()
    {
        base.Start();

        charge = new MeleeNormal_Charge(stateMachine, this, "charge", chargeData, this);
        dead = new MeleeNormal_Dead(stateMachine, this, "dead", deadData, this);
        detected = new MeleeNormal_Detected(stateMachine, this, "detected", detectedData, this);
        findPlayer = new MeleeNormal_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        hurt = new MeleeNormal_Hurt(stateMachine, this, "hurt", hurtData, this);
        idle = new MeleeNormal_Idle(stateMachine, this, "idle", idleData, this);
        meleeAttack = new MeleeNormal_MeleeAttack(stateMachine, this, "meleeAttack", sense.meleeAttackCheck, meleeAttackData, this);
        move = new MeleeNormal_Move(stateMachine, this, "move", moveData, this);
        stun = new MeleeNormal_Stun(stateMachine, this, "stun", stunData, this);
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