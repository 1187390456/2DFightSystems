using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickWeapon : AggressiveWeapon
{
    public override void Enter()
    {
        base.Enter();
        if (attackIndex >= weaponData.amountOfAttacks) attackIndex = 0;
        baseAt.SetInteger("attackIndex", attackIndex);
        weaponAt.SetInteger("attackIndex", attackIndex);
    }

    public override void Exit()
    {
        base.Exit();
        attackIndex++;
    }
}