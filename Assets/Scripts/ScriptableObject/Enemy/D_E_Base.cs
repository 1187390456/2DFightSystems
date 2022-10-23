using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

[CreateAssetMenu(fileName = "Base", menuName = "Data/Enemy Data/State Data/Base Data")]
public class D_E_Base : ScriptableObject
{
    [Header("墙壁检测距离")] public float wallCheckDistance = 0.1f;
    [Header("边缘检测距离")] public float edgeCheckDistance = 1.0f;
    [Header("地面检测距离")] public float groundCheckDistance = 0.1f;
    [Header("最小警备距离")] public float minDetectedDistance = 6.5f;
    [Header("最大警备距离")] public float maxDetectedDistance = 9.5f;
    [Header("可近战攻击的距离")] public float canMeleeDistance = 2.0f;
    [Header("击晕次数")] public int stunCount = 5;
    [Header("眩晕中Y轴击退速度")] public float stunKnockbackSpeedY = 15.0f;
    [Header("最大生命值")] public float maxHealth = 100.0f;
    [Header("被击打特效")] public GameObject effectRes = null;
    [Header("能否使用闪避")] public bool canDodge = false;
    [Header("能否使用技能1")] public bool canAbility1 = false;
}