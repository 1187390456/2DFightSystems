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

    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("冲锋数据")] public D_E_Charge chargeData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;

    private void Start()
    {
        move = new WildBoar_Move(stateMachine, this, "move", moveData, this);
        idle = new WildBoar_Idle(stateMachine, this, "idle", idleData, this);
        detected = new WildBoar_Detected(stateMachine, this, "detected", detectedData, this);
        charge = new WildBoar_Charge(stateMachine, this, "charge", chargeData, this);
        findPlayer = new WildBoar_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        stateMachine.Init(move);
    }

    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}