using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : E_Entity
{
    public Monster_Dead dead; // 死亡
    public Monster_Idle idle; // 移动
    public Monster_Move move; // 空闲
    public Monster_Detected detected; // 警备
    public Monster_Charge charge; // 冲锋
    public Monster_MeleeAttack meleeAttack; // 近战攻击
    public Monster_FindPlayer findPlayer; // 寻找玩家
    public Monster_Ability1 ability1; // 技能1

    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("冲锋数据")] public D_E_Charge chargeData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("技能1数据")] public D_E_Ability1 ability1Data;

    private void Start()
    {
        move = new Monster_Move(stateMachine, this, "move", moveData, this);
        idle = new Monster_Idle(stateMachine, this, "idle", idleData, this);
        dead = new Monster_Dead(stateMachine, this, "dead", deadData, this);
        detected = new Monster_Detected(stateMachine, this, "detected", detectedData, this);
        charge = new Monster_Charge(stateMachine, this, "charge", chargeData, this);
        findPlayer = new Monster_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        meleeAttack = new Monster_MeleeAttack(stateMachine, this, "meleeAttack", meleeAttackCheck, meleeAttackData, this);
        ability1 = new Monster_Ability1(stateMachine, this, "ability1", ability1Data, this);
        stateMachine.Init(move);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackCheck.transform.position, meleeAttackData.meleeAttackRadius);
    }

    // 检测死亡
    private bool CheckDead()
    {
        return isDead && stateMachine.currentState != dead;
    }

    // 检测受伤
    private bool CheckHurt()
    {
        return isHurting && stateMachine.currentState != detected;
    }

    public override void Update()
    {
        base.Update();
        if (CheckDead())
        {
            stateMachine.ChangeState(dead);
        }
        else if (CheckHurt())
        {
            stateMachine.ChangeState(detected);
        }
    }
}