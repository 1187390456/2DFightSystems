using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Data/Enemy State Data/Dash Data")]
public class D_E_Dash : ScriptableObject
{
    [Header("冲刺速度")] public float dashSpeed = 13.0f;
}
