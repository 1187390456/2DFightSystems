using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : CoreComponent
{
    [Header("玩家数据")] public D_P_Base playerData;
    protected Player player;
    public P_StateMachine stateMachine = new P_StateMachine();
    public P_Idle idle { get; private set; }
    public P_Move move { get; private set; }
    public P_Jump jump { get; private set; }
    public P_InAir inAir { get; private set; }
    public P_Land land { get; private set; }
    public P_Silde slide { get; private set; }
    public P_Catch catchWall { get; private set; }
    public P_Climb climb { get; private set; }
    public P_WallJump wallJump { get; private set; }
    public P_Ledge ledge { get; private set; }
    public P_Dash dash { get; private set; }
    public P_CrouchIdle crouchIdle { get; private set; }
    public P_CrouchMove crouchMove { get; private set; }
    public P_KnockBack knockBack { get; private set; }
    public P_Dead dead { get; private set; }
    public P_Attack firstAttack { get; private set; }
    public P_Attack secondAttack { get; private set; }

    public override void Awake()
    {
        base.Awake();
        player = target.GetComponent<Player>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        stateMachine.currentState.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public override void Start()
    {
        base.Start();
        idle = new P_Idle(stateMachine, player, "idle", playerData);
        move = new P_Move(stateMachine, player, "move", playerData);
        jump = new P_Jump(stateMachine, player, "inAir", playerData);
        inAir = new P_InAir(stateMachine, player, "inAir", playerData);
        land = new P_Land(stateMachine, player, "land", playerData);
        slide = new P_Silde(stateMachine, player, "silde", playerData);
        catchWall = new P_Catch(stateMachine, player, "catch", playerData);
        climb = new P_Climb(stateMachine, player, "climb", playerData);
        wallJump = new P_WallJump(stateMachine, player, "inAir", playerData);
        ledge = new P_Ledge(stateMachine, player, "ledgeState", playerData);
        dash = new P_Dash(stateMachine, player, "inAir", playerData);
        crouchIdle = new P_CrouchIdle(stateMachine, player, "crouchIdle", playerData);
        crouchMove = new P_CrouchMove(stateMachine, player, "crouchMove", playerData);
        knockBack = new P_KnockBack(stateMachine, player, "knockBack", playerData);
        dead = new P_Dead(stateMachine, player, "dead", playerData);
        firstAttack = new P_Attack(stateMachine, player, "attack", playerData);
        secondAttack = new P_Attack(stateMachine, player, "attack", playerData);
        stateMachine.Init(idle);

        firstAttack.SetWeapon(player.weaponInventory.weapons[0]);

        // Debug.Log(player.weaponInventory.weapons[0]);
    }
}