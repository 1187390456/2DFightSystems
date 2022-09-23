using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hurt", menuName = "Data/Enemy Data/State Data/Hurt Data")]
public class D_E_Hurt : ScriptableObject
{
    [Header("受伤暂停时间")] public float hurtTime = 0.2f;
}