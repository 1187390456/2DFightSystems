using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "Data/Enemy State Data/Idle Data")]
public class D_E_Idle : ScriptableObject
{
    [Header("空闲时间")] public float idleTime = 2f;
}
