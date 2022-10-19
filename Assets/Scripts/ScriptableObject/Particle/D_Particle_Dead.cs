using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dead", menuName = "Data/Particle Data/Combat Particle/Dead Particle")]
public class D_Particle_Dead : ScriptableObject
{
    [Header("死亡")] public GameObject[] dead;
}