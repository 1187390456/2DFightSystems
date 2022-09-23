using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindPlayer", menuName = "Data/Enemy Data/State Data/FindPlayer Data")]
public class D_E_FindPlayer : ScriptableObject
{
    [Header("每次转身停留时间")] public float turnTimeSpace = 0.8f;
}