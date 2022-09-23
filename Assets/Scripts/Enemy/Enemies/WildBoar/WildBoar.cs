using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar : E_Entity
{
    public static WildBoar Instance { get; private set; }

    public WildBoar_Move move; // 移动
    public WildBoar_Idle idle; // 空闲
    public WildBoar_Detected detected; // 警备
    public WildBoar_Charge charge; // 冲锋
    public WildBoar_FindPlayer findPlayer; // 寻找玩家
    public WildBoar_MeleeAttack meleeAttack; // 近战攻击
    public WildBoar_Stun stun; // 眩晕
    public WildBoar_Hurt hurt; // 受伤

    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("冲锋数据")] public D_E_Charge chargeData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("眩晕数据")] public D_E_Stun stunData;
    [Header("受伤数据")] public D_E_Hurt hurtData;

    private void Start()
    {
        move = new WildBoar_Move(stateMachine, this, "move", moveData, this);
        idle = new WildBoar_Idle(stateMachine, this, "idle", idleData, this);
        detected = new WildBoar_Detected(stateMachine, this, "detected", detectedData, this);
        charge = new WildBoar_Charge(stateMachine, this, "charge", chargeData, this);
        findPlayer = new WildBoar_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        meleeAttack = new WildBoar_MeleeAttack(stateMachine, this, "meleeAttack", meleeAttackCheck, meleeAttackData, this);
        stun = new WildBoar_Stun(stateMachine, this, "stun", stunData, this);
        hurt = new WildBoar_Hurt(stateMachine, this, "hurt", hurtData, this);
        stateMachine.Init(move);
    }

    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackCheck.transform.position, meleeAttackData.meleeAttackRadius);
    }

    public override void Update()
    {
        base.Update();
        if (canEnterStun && !isStuning)
        {
            canEnterStun = false;
            isStuning = true;
            stateMachine.ChangeState(stun);
        }
        if (canEnterHurt && !isHurting)
        {
            canEnterHurt = false;
            isHurting = true;
            stateMachine.ChangeState(hurt);
        }
    }
}