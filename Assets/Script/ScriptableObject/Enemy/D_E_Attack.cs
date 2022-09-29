using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Data/Enemy State Data/Attack Data")]
public class D_E_Attack : ScriptableObject
{
    [Header("攻击")] public float attack = 0.0f;
}
