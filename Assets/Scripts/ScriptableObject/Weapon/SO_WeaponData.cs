using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    [Header("攻击移动速度")] public float[] moveSpeed;
}