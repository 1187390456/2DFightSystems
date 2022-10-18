using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CoreComponent
{
    public E_StateMachine stateMachine = new E_StateMachine();
    public Enemy enemy { get; private set; }

    [Header("实体数据")] public D_E_Base entityData;
    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("眩晕数据")] public D_E_Stun stunData;
    [Header("闪避数据")] public D_E_Dodge dodgeData;
    [Header("受伤数据")] public D_E_Hurt hurtData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("远程攻击数据")] public D_E_RemoteAttack remoteAttackData;
    [Header("技能1数据")] public D_E_Ability1 ability1Data;
    [Header("技能2数据")] public D_E_Ability2 ability2Data;
    [Header("追击数据")] public D_E_Charge chargeData;

    public E_S_Dead dead; // 死亡
    public E_S_Idle idle; // 空闲
    public E_S_Move move; // 移动
    public E_S_Stun stun; // 眩晕
    public E_S_Hurt hurt; // 受伤
    public E_S_Dodge dodge; // 闪避
    public E_S_Detected detected; // 警备
    public E_S_FindPlayer findPlayer; // 寻找玩家
    public E_S_MeleeAttack meleeAttack; // 近战攻击
    public E_S_RemoteAttack remoteAttack; // 远程攻击
    public E_S_Ability1 ability1; // 技能1
    public E_S_Ability2 ability2;
    public E_S_Charge charge; // 追击

    public override void Awake()
    {
        base.Awake();
        enemy = target.GetComponent<Enemy>();
    }

    public override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        stateMachine.currentState.FixUpdate();
    }

    public override void Start()
    {
        base.Start();
        dead = new E_S_Dead(stateMachine, enemy, "dead", deadData);
        idle = new E_S_Idle(stateMachine, enemy, "idle", idleData);
        move = new E_S_Move(stateMachine, enemy, "move", moveData);
        stun = new E_S_Stun(stateMachine, enemy, "stun", stunData);
        hurt = new E_S_Hurt(stateMachine, enemy, "hurt", hurtData);
        dodge = new E_S_Dodge(stateMachine, enemy, "dodge", dodgeData);
        detected = new E_S_Detected(stateMachine, enemy, "detected", detectedData);
        findPlayer = new E_S_FindPlayer(stateMachine, enemy, "findPlayer", findPlayerData);
        meleeAttack = new E_S_MeleeAttack(stateMachine, enemy, "meleeAttack", enemySense.meleeAttackCheck, meleeAttackData);
        remoteAttack = new E_S_RemoteAttack(stateMachine, enemy, "remoteAttack", enemySense.remoteAttackCheck, remoteAttackData);
        ability1 = new E_S_Ability1(stateMachine, enemy, "ability1", ability1Data);
        ability2 = new E_S_Ability2(stateMachine, enemy, "ability2", enemySense.remoteAttackCheck, remoteAttackData, ability2Data);
        charge = new E_S_Charge(stateMachine, enemy, "charge", chargeData);
        stateMachine.Init(move);
    }
}