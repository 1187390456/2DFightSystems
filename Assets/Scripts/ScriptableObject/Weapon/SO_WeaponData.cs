using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    public int amountOfAttacks { get; protected set; }
    public float[] moveSpeed { get; protected set; }
}