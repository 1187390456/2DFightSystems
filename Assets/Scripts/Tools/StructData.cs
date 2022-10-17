using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo
{
    public float damage;
    public float damageSourcePosX;
}

[System.Serializable]
public struct WeaponAttackInfo
{
    public string attackName;
    public float moveSpeed;
    public float damageAmount;
}