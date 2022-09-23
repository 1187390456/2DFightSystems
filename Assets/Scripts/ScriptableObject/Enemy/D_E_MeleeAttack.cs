using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Data/Enemy Data/State Data/MeleeAttack Data")]
public class D_E_MeleeAttack : ScriptableObject
{
    [Header("近战攻击半径")] public float meleeAttackRadius = 1.0f;
    [Header("近战攻击伤害")] public float meleeAttackDamage = 10.0f;
}