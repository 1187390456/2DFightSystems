using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability2", menuName = "Data/Enemy Data/State Data/Ability2 Data")]
public class D_E_Ability2 : ScriptableObject
{
    [Header("持续时间")] public float ability2Time = 10.0f;
}