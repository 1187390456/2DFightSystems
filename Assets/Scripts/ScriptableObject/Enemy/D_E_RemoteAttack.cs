using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RemoteAttack", menuName = "Data/Enemy Data/State Data/RemoteAttack Data")]
public class D_E_RemoteAttack : ScriptableObject
{
    [Header("远程攻击伤害")] public float damage = 10.0f;
    [Header("子弹飞行速度")] public float speed = 20.0f;
    [Header("子弹飞行距离")] public float distance = 10.0f;
    [Header("子弹下降速度(重力大小)")] public float gravityScale = 8.0f;
    [Header("子弹预制件")] public GameObject bullet;
}