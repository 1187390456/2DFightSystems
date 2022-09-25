using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RemoteAttack", menuName = "Data/Enemy Data/State Data/RemoteAttack Data")]
public class D_E_RemoteAttack : ScriptableObject
{
    [Header("远程攻击伤害")] public float remoteAttackDamage = 10.0f;
}