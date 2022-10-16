using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Attack : P_Ability
{
    protected Weapon weapon;

    public P_Attack(P_StateMachine stateMachine, Player player, string anmName, D_P_Base playerData) : base(stateMachine, player, anmName, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.Enter();
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
        weapon.InitState(this);
    }
}