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
        movement.SetVelocityX(velocity * movement.facingDireciton);
        velocitySet = velocity;
        isMoving = true;
    }

    public void MoveStop(float velocity)
    {
        movement.SetVelocityX(velocity * movement.facingDireciton);
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
            movement.CheckTurn();
        }

        if (isMoving)
        {
            movement.SetVelocityX(velocitySet * movement.facingDireciton);
        }
    }
}