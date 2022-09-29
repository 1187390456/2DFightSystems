using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar : E_Entity
{
    public WildBoar_Move move;//移动
    public WildBoar_Idle idle;//空闲
    public WildBoar_Vigilant vigilant;//警惕
    public WildBoar_Dash dash;//冲刺
    public WildBoar_Attack attack;//攻击
    public D_E_Move moveData;//移动数据
    public D_E_Idle idleData;//空闲数据
    public D_E_Vigilant vigilantData;//警惕数据
    public D_E_Dash dashData;//冲刺数据
    public D_E_Attack attackData;//攻击数据

    public override void Start()
    {
        base.Start();
        move = new WildBoar_Move(stateMachine, this, "move", moveData, this);
        idle = new WildBoar_Idle(stateMachine, this, "idle", idleData, this);
        vigilant = new WildBoar_Vigilant(stateMachine, this, "vigilant", vigilantData, this);
        dash = new WildBoar_Dash(stateMachine, this, "dash", dashData, this);
        attack = new WildBoar_Attack(stateMachine, this, "attack", attackData, this);
        stateMachine.InitState(move);
    }
}
