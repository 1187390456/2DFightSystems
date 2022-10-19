using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Environment", menuName = "Data/Particle Data/Environment Particle")]
public class D_Particle_Environment : ScriptableObject
{
    [Header("水 2D")] public GameObject[] water2D;
    [Header("天气 2D")] public GameObject[] weather2D;
    [Header("泡沫")] public GameObject[] bubbles;
    [Header("纸屑")] public GameObject[] confetti;
    [Header("灰尘")] public GameObject[] dust;
    [Header("火")] public GameObject[] fire;
    [Header("火种")] public GameObject[] fireFiles;
    [Header("烟花")] public GameObject[] firework;
    [Header("雾气")] public GameObject[] fog;
    [Header("闪电")] public GameObject[] lightning;
    [Header("烟雾")] public GameObject[] Smoke;
    [Header("火花")] public GameObject[] spark;
    [Header("星星")] public GameObject[] start;
    [Header("水底")] public GameObject[] underWater;
    [Header("水")] public GameObject[] water;
    [Header("天气")] public GameObject[] weather;
}