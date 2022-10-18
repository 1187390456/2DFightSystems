using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Data/Enemy Data/State Data/MeleeAttack Data")]
public class D_E_MeleeAttack : ScriptableObject
{
    [Header("近战攻击伤害")] public float meleeAttackDamage = 10.0f;
    [Header("击退角度")] public Vector2 knockbackAngle = new Vector2(1.0f, 1.0f);
    [Header("击退速度")] public float knockbackSpeed = 10.0f;
}