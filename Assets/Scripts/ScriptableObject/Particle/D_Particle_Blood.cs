using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blood", menuName = "Data/Particle Data/Combat Particle/Blood Particle")]
public class D_Particle_Blood : ScriptableObject
{
    [Header("2D 血 - 绿色")] public GameObject[] bloodGreen2D;
    [Header("2D 血")] public GameObject[] blood2D;
    [Header("2D 血 - 黄色")] public GameObject[] bloodYellow2D;
    [Header("血 - 绿色")] public GameObject[] bloodGreen;
    [Header("血")] public GameObject[] blood;
    [Header("血 - 黄色")] public GameObject[] bloodYellow;
}