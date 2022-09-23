using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charge", menuName = "Data/Enemy Data/State Data/Charge Data")]
public class D_E_Charge : ScriptableObject
{
    [Header("冲锋时间")] public float chargeTime = 0.8f;
    [Header("冲锋速度")] public float chargeSpeed = 10.0f;
}