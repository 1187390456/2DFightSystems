using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponAttackInfo
{
    public string attackName;
    public float moveSpeed;
    public float damageAmount;

    public float knockbackSpeed;
    public Vector2 knockbackAngle;
}