using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : E_Entity
{
    [Header("远程攻击检测点")] public Transform remoteAttackCheck;
    public static Archer Instance { get; private set; }

    public Archer_Dead dead; // 死亡
    public Archer_Idle idle; // 空闲
    public Archer_Move move; // 移动
    public Archer_Stun stun; // 眩晕
    public Archer_Dodge dodge; // 闪避
    public Archer_Detected detected; // 警备
    public Archer_FindPlayer findPlayer; // 寻找玩家
    public Archer_MeleeAttack meleeAttack; // 近战攻击
    public Archer_RemoteAttack remoteAttack; // 远程攻击
    public Archer_Ability1 ability1; // 技能1

    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("眩晕数据")] public D_E_Stun stunData;
    [Header("闪避数据")] public D_E_Dodge dodgeData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("远程攻击数据")] public D_E_RemoteAttack remoteAttackData;
    [Header("技能1数据")] public D_E_Ability1 ability1Data;

    private void Start()
    {
        idle = new Archer_Idle(stateMachine, this, "idle", idleData, this);
        move = new Archer_Move(stateMachine, this, "move", moveData, this);
        dead = new Archer_Dead(stateMachine, this, "dead", deadData, this);
        stun = new Archer_Stun(stateMachine, this, "stun", stunData, this);
        detected = new Archer_Detected(stateMachine, this, "detected", detectedData, this);
        findPlayer = new Archer_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        meleeAttack = new Archer_MeleeAttack(stateMachine, this, "meleeAttack", meleeAttackCheck, meleeAttackData, this);
        remoteAttack = new Archer_RemoteAttack(stateMachine, this, "remoteAttack", remoteAttackCheck, remoteAttackData, this);
        dodge = new Archer_Dodge(stateMachine, this, "dodge", dodgeData, this);
        ability1 = new Archer_Ability1(stateMachine, this, "ability1", ability1Data, this);
        stateMachine.Init(move);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackCheck.transform.position, meleeAttackData.meleeAttackRadius);
    }

    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void Update()
    {
        base.Update();
        UpdateAnimation();
        if (CheckDead())
        {
            stateMachine.ChangeState(dead);
        }
        else if (CheckStun())
        {
            stateMachine.ChangeState(stun);
        }
        else if (CheckHurt())
        {
            stateMachine.ChangeState(detected);
        }
    }

    // 更新动画
    private void UpdateAnimation()
    {
        at.SetFloat("yVelocity", rb.velocity.y);
    }

    // 检测死亡
    private bool CheckDead()
    {
        return isDead && stateMachine.currentState != dead;
    }

    // 检测眩晕
    private bool CheckStun()
    {
        return isStuning && stateMachine.currentState != stun;
    }

    // 检测受伤
    private bool CheckHurt()
    {
        return isHurting && stateMachine.currentState != detected;
    }

    // 检测是否可以闪避
    public bool CheckCanDodge()
    {
        if (Time.time >= dodge.endDodgeTime + dodge.dodgeData.cooldown)
        {
            return true;
        }
        return false;
    }
}