using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AggressiveWeapon", menuName = "Data/Weapon Data/AggressiveWeapon")]
public class SO_AggressionWeaponData : SO_WeaponData
{
    public WeaponAttackInfo[] attackInfos;

    private void OnEnable()
    {
        amountOfAttacks = attackInfos.Length;
        moveSpeed = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++)
        {
            moveSpeed[i] = attackInfos[i].moveSpeed;
        }
    }
}