using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dodge", menuName = "Data/Enemy Data/State Data/Dodge Data")]
public class D_E_Dodge : ScriptableObject
{
    [Header("闪避速度")] public float speed = 25.0f;
    [Header("闪避角度")] public Vector2 angle = new Vector2(1.0f, 1.0f);
    [Header("闪避时间")] public float dodgeTime = 0.2f;
    [Header("闪避冷却时间")] public float cooldown = 1.0f;
}