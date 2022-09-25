using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : E_Entity
{
    public static Archer Instance { get; private set; }

    public Archer_Dead dead; // 死亡
    public Archer_FindPlayer findPlayer; // 寻找玩家
    public Archer_Idle idle; // 空闲
    public Archer_MeleeAttack meleeAttack; // 近战攻击
    public Archer_Move move; // 移动
    public Archer_RemoteAttack remoteAttack; // 远程攻击
    public Archer_Stun stun; // 眩晕

    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("眩晕数据")] public D_E_Stun stunData;
    [Header("远程攻击数据")] public D_E_RemoteAttack remoteAttackData;

    private void Start()
    {
        idle = new Archer_Idle(stateMachine, this, "Idle", idleData, this);
        move = new Archer_Move(stateMachine, this, "move", moveData, this);
        meleeAttack = new Archer_MeleeAttack(stateMachine, this, "meleeAttack", meleeAttackCheck, meleeAttackData, this);
        dead = new Archer_Dead(stateMachine, this, "dead", deadData, this);
        findPlayer = new Archer_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        stun = new Archer_Stun(stateMachine, this, "stun", stunData, this);
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
        if (isDead && stateMachine.currentState != dead)
        {
            stateMachine.ChangeState(dead);
        }
        else if (isStuning && stateMachine.currentState != stun)
        {
            stateMachine.ChangeState(stun);
        }
    }
}