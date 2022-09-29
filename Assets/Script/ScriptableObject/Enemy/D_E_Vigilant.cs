using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vigilant", menuName = "Data/Enemy State Data/Vigilant Data")]
public class D_E_Vigilant : ScriptableObject
{
    [Header("警惕时间")] public float vigilantTime = 1f;
}
