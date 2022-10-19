using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flamethrower", menuName = "Data/Particle Data/Combat Particle/Flamethrower Particle")]
public class D_Particle_Flamethrower : ScriptableObject
{
    [Header("卡通")] public GameObject[] cartoon;
    [Header("柔和")] public GameObject[] soft;
    [Header("突兀")] public GameObject[] spiky;
}