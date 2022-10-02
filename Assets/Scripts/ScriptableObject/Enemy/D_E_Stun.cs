using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun", menuName = "Data/Enemy Data/State Data/Stun Data")]
public class D_E_Stun : ScriptableObject
{
    [Header("眩晕时间")] public float stunTime = 1.5f;
    [Header("眩晕击退速度")] public float stunKnockBackVelocity = 15.0f;
    [Header("眩晕击退角度")] public Vector2 stunKnockBackAngle = new Vector2(1.0f, 2.0f);
    [Header("是否需要特效")] public bool isNeedEffect = true;
}