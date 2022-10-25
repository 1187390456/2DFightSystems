using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickWeapon : AggressiveWeapon
{
    public override void AnimationStart()
    {
        base.AnimationStart();
        WeaponAttackInfo weaponAttackInfo = aggressionWeaponData.attackInfos[attackIndex];
        var randomIndex = Random.Range(0, weaponAttackInfo.effectArr.Length);
        var rot = Quaternion.Euler(0.0f, 0.0f, weaponAttackInfo.rot);
        Debug.Log(weaponAttackInfo.effectArr[randomIndex].name);
        if (weaponAttackInfo.attackName == "Fist")
        {
            rot = Quaternion.Euler(0.0f, -90.0f, 90.0f);
        }
        var gobj = ParticleManager.instance.CreateParticle(weaponAttackInfo.effectArr[randomIndex], transform, rot);
        if (weaponAttackInfo.attackName == "Fist")
        {
            gobj.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f * -Player.Instance.movement.facingDireciton);
        }
        else
        {
            gobj.transform.localScale = new Vector3(Player.Instance.movement.facingDireciton, -Player.Instance.movement.facingDireciton, 1);
        }
    }

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