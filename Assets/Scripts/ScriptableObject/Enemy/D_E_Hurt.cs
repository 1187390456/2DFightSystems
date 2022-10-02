using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hurt", menuName = "Data/Enemy Data/State Data/Hurt Data")]
public class D_E_Hurt : ScriptableObject
{
    [Header("受伤暂停时间")] public float hurtTime = 0.2f;
    [Header("受伤冷却")] public float hurtCoolDown = 5.0f;
    [Header("受伤击退速度")] public float knockbackSpeed = 8.0f;
    [Header("受伤击退角度")] public Vector2 knockbackAngle = new Vector2(1.0f, 1.0f);
}