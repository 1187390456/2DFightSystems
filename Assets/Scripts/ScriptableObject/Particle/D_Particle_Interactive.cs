using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactive", menuName = "Data/Particle Data/Interactive Particle")]
public class D_Particle_Interactive : ScriptableObject
{
    [Header("卡片")] public GameObject[] card;
    [Header("表情")] public GameObject[] emoji;
    [Header("羽毛")] public GameObject[] feather;
    [Header("信号弹")] public GameObject[] flare;
    [Header("治愈")] public GameObject[] healing;
    [Header("爱心")] public GameObject[] heart;
    [Header("战利品")] public GameObject[] loot;
    [Header("钱")] public GameObject[] money;
    [Header("传送门")] public GameObject[] portals;
    [Header("能量汲取")] public GameObject[] powerups;
    [Header("闪耀")] public GameObject[] sparkle;
    [Header("星星")] public GameObject[] star;
    [Header("实验")] public GameObject[] trails;
    [Header("区域")] public GameObject[] zone;
}