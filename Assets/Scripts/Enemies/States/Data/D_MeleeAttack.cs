using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]

public class D_MeleeAttack : ScriptableObject
{
    public float attackRadius = 0.5f;//攻击半径
    public float attackDamage = 10f;//攻击伤害

    public LayerMask whatIsPlayer;
}
