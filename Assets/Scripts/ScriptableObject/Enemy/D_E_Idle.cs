using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "Data/Enemy Data/State Data/Idle Data")]
public class D_E_Idle : ScriptableObject
{
    [Header("最大空闲时间")] public float maxIdleTime = 3.0f;
    [Header("最小空闲时间")] public float minIdleTime = 1.0f;
}