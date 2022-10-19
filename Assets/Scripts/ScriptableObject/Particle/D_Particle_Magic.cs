using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic", menuName = "Data/Particle Data/Combat Particle/Magic Particle")]
public class D_Particle_Magic : ScriptableObject
{
    [Header("光环")] public GameObject[] aura;
    [Header("光环 - 柔和")] public GameObject[] auraSoft;
    [Header("增益")] public GameObject[] buff;
    [Header("电荷汲取")] public GameObject[] charge;
    [Header("圆形光环")] public GameObject[] circle;
    [Header("圆形光环 - 样例")] public GameObject[] circleSimple;
    [Header("附魔")] public GameObject[] enchant;
    [Header("领域光环")] public GameObject[] field;
    [Header("新星")] public GameObject[] nova;
    [Header("柱形爆炸")] public GameObject[] pillarBlast;
    [Header("盾")] public GameObject[] shield;
    [Header("球形")] public GameObject[] sphere;
}