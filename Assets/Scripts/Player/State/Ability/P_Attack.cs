using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Attack : P_Ability
{
    protected Weapon weapon;
    protected bool isMoving;
    protected bool canTurn;
    protected float velocitySet;

    public P_Attack(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.Enter();
        isMoving = false;
    }

    public override void Exit()
    {
        base.Exit();
        weapon.Exit();
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
        isAbilityDone = true;
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        this.weapon.InitState(this);
    }

    public void MoveStart(float velocity)
    {
        player.SetVelocityX(velocity * player.facingDireciton);
        velocitySet = velocity;
        isMoving = true;
    }

    public void MoveStop(float velocity)
    {
        player.SetVelocityX(velocity * player.facingDireciton);
        isMoving = false;
    }

    public void SetTurnOff()
    {
        canTurn = false;
    }

    public void SetTurnOn()
    {
        canTurn = true;
    }

    public override void Update()
    {
        base.Update();
        if (canTurn)
        {
            player.CheckTurn();
        }

        if (isMoving)
        {
            player.SetVelocityX(velocitySet * player.facingDireciton);
        }
    }
}