using UnityEngine;

public class E_FindPlayer : E_State
{
    protected D_E_FindPlayer findPlayerData; // 寻找玩家数据
    protected bool findPlayerTimeOver; // 寻找玩家状态是否结束

    protected float lastTurnTime; // 上次转身时间
    protected int turnCount; // 转身次数
    protected bool isFindPlayer; // 是否找到玩家

    public E_FindPlayer(E_StateMachine stateMachine, Enemy entity, string anmName, D_E_FindPlayer findPlayerData) : base(stateMachine, entity, anmName)
    {
        this.findPlayerData = findPlayerData;
    }

    public override void Enter()
    {
        base.Enter();
        findPlayerTimeOver = false;
        turnCount = 3;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        movement.SetVelocityX(0.0f);
        if (startTime + Time.deltaTime >= startTime + 0.8f)
        {
            Turn();
        }
        if (Time.time >= lastTurnTime + findPlayerData.turnTimeSpace && turnCount > 0)
        {
            Turn();
        }
        else if (turnCount <= 0)
        {
            isFindPlayer = false;
            findPlayerTimeOver = true;
        }
    }

    private void Turn()
    {
        movement.SetTurn();
        turnCount--;
        lastTurnTime = Time.time;
        if (sense.MinDetected())
        {
            isFindPlayer = true;
            findPlayerTimeOver = true;
        }
    }
}