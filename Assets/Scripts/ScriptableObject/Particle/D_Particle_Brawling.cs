using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brawling", menuName = "Data/Particle Data/Combat Particle/Brawling Particle")]
public class D_Particle_Brawling : ScriptableObject
{
    [Header("卡通攻击")] public GameObject[] cartoony;
    [Header("圆形攻击")] public GameObject[] round;
    [Header("柔和攻击")] public GameObject[] soft;
    [Header("眩晕")] public GameObject[] stun;
}