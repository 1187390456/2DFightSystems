using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decal", menuName = "Data/Particle Data/Combat Particle/Decal Particle")]
public class D_Particle_Decal : ScriptableObject
{
    [Header("子弹创痕")] public GameObject[] bullet;
    [Header("能量创痕")] public GameObject[] energy;
    [Header("爆炸创痕")] public GameObject[] explosion;
}