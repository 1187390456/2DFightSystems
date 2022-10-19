using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova", menuName = "Data/Particle Data/Combat Particle/Nova Particle")]
public class D_Particle_Nova : ScriptableObject
{
    [Header("火光")] public GameObject[] fire;
    [Header("冰霜")] public GameObject[] frost;
    [Header("电光")] public GameObject[] lightning;
    [Header("标准")] public GameObject[] standard;
}