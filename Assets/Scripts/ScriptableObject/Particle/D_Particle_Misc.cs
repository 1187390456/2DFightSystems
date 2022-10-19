using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Misc", menuName = "Data/Particle Data/Misc Particle")]
public class D_Particle_Misc : ScriptableObject
{
    [Header("光环")] public GameObject auraTest;
    [Header("弯曲的烟雾")] public GameObject curvySmoke;
    [Header("音乐爆炸")] public GameObject musicalNoteExplosion;
    [Header("音乐")] public GameObject musicalNote;
    [Header("神经")] public GameObject plexus;
    [Header("神经爆炸")] public GameObject plexusExplosion;
    [Header("发送消息")] public GameObject typingMessage;
    [Header("发送消息闪光")] public GameObject typingMessageLight;
    [Header("文字")] public GameObject[] text;
    [Header("盾")] public GameObject[] shield;
    [Header("喷射堆")] public GameObject[] bombFuse;
}