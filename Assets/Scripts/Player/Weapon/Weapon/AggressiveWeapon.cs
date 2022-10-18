using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressionWeaponData aggressionWeaponData;
    private List<IDamageable> damageList = new List<IDamageable>();

    public override void AnimationStart()
    {
        base.AnimationStart();
        StartMeleeAttack();
    }

    private void StartMeleeAttack()
    {
        WeaponAttackInfo weaponAttackInfo = aggressionWeaponData.attackInfos[attackIndex];
        foreach (var item in damageList.ToList())
        {
            var attckInfo = new AttackInfo
            {
                damage = weaponAttackInfo.damageAmount,
                damageSourcePosX = GetComponentInParent<Player>().transform.position.x,
            };
            item.AcceptPlayerDamage(attckInfo);
        }
    }

    public void AddList(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageList.Add(damageable);
        }
    }

    public void RemoveList(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageList.Remove(damageable);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (weaponData.GetType() == typeof(SO_AggressionWeaponData))
        {
            aggressionWeaponData = (SO_AggressionWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("错误数据引用");
        }
    }
}