using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun", menuName = "Data/Enemy Data/State Data/Stun Data")]
public class D_E_Stun : ScriptableObject
{
    [Header("眩晕时间")] public float stunTime = 1.5f;
    [Header("是否需要特效")] public bool isNeedEffect = true;
}