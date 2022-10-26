using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressionWeaponData aggressionWeaponData;
    private List<IDamageable> damageList = new List<IDamageable>();
    private List<IKnockbackable> knockbackList = new List<IKnockbackable>();

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
            item.Damage(weaponAttackInfo.damageAmount);
        }
        foreach (var item in knockbackList.ToList())
        {
            item.Knckback(weaponAttackInfo.knockbackSpeed, weaponAttackInfo.knockbackAngle, movement.facingDireciton);
        }
    }

    public void AddList(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageList.Add(damageable);
        }
        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        if (knockbackable != null)
        {
            knockbackList.Add(knockbackable);
        }
    }

    public void RemoveList(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageList.Remove(damageable);
        }
        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        if (knockbackable != null)
        {
            knockbackList.Remove(knockbackable);
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