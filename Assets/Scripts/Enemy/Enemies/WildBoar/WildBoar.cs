using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar : E_Entity
{
    public WildBoar_Move move; // 移动
    public WildBoar_Idle idle; // 空闲
    public WildBoar_Detected detected; // 警备

    [Header("敌人空闲数据")] public D_E_Idle idleData;
    [Header("敌人移动数据")] public D_E_Move moveData;
    [Header("敌人警备数据")] public D_E_Detected detectedData;

    private void Start()
    {
        move = new WildBoar_Move(stateMachine, this, "move", moveData, this);
        idle = new WildBoar_Idle(stateMachine, this, "idle", idleData, this);
        detected = new WildBoar_Detected(stateMachine, this, "detected", detectedData, this);
        stateMachine.Init(move);
    }
}