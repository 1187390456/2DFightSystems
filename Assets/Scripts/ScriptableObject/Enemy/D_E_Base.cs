using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy Data/State Data/Base Data")]
public class D_E_Base : ScriptableObject
{
    [Header("墙壁检测距离")] public float wallCheckDistance = 0.1f;
    [Header("边缘检测距离")] public float edgeCheckDistance = 0.1f;
    [Header("最小警备距离")] public float minDetectedDistance = 3.0f;
    [Header("最大警备距离")] public float maxDetectedDistance = 4.0f;
}