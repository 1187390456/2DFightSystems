using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Data/Particle Data/Combat Particle/Sword Particle")]
public class D_Particle_Sword : ScriptableObject
{
    [Header("遗留")] public GameObject[] legacy;
    [Header("击打")] public GameObject[] hit;
    [Header("斜线")] public GameObject[] slash;
    [Header("旋转")] public GameObject[] spin;
    [Header("波浪")] public GameObject[] wave;
    [Header("旋风")] public GameObject[] whirlwind;
}