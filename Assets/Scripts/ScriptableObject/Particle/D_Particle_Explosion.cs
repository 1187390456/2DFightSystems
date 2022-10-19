using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion", menuName = "Data/Particle Data/Combat Particle/Explosion Particle")]
public class D_Particle_Explosion : ScriptableObject
{
    [Header("2D 爆炸")] public GameObject[] explosions2D;
    [Header("爆炸 - 其他")] public GameObject[] misc;
    [Header("爆炸 - 子弹")] public GameObject[] bulletExplosion;
    [Header("爆炸 - 大")] public GameObject[] fatExplosion;
    [Header("爆炸 - 火球")] public GameObject[] fireBallExplosion;
    [Header("爆炸 - 冰")] public GameObject[] frostExplosion;
    [Header("爆炸 - 烟雾")] public GameObject[] gasExplosion;
    [Header("爆炸 - 闪光")] public GameObject[] glitterExplosion;
    [Header("爆炸 - 手雷")] public GameObject[] grenadeExplosion;
    [Header("爆炸 - 激光")] public GameObject[] laserExplosion;
    [Header("爆炸 - 闪电")] public GameObject[] lightningExplosion;
    [Header("爆炸 - 液体")] public GameObject[] liquidExplosion;
    [Header("爆炸 - 魔法")] public GameObject[] magicExplosion;
    [Header("爆炸 - 魔法 - 柔和")] public GameObject[] magicSoftExplosion;
    [Header("爆炸 - 巨型")] public GameObject[] megaExplosion;
    [Header("爆炸 - 神秘")] public GameObject[] mysticExplosion;
    [Header("爆炸 - 新星")] public GameObject[] novaExplosion;
    [Header("爆炸 - 新星 - 小")] public GameObject[] novaSmallExplosion;
    [Header("爆炸 - 核弹 - 散开")] public GameObject[] nukeConeExplosion;
    [Header("爆炸 - 核弹")] public GameObject[] nukeExplosion;
    [Header("爆炸 - 核弹 - 垂直")] public GameObject[] nukeVerticalExplosion;
    [Header("爆炸 - 圆形")] public GameObject[] roundExplosion;
    [Header("爆炸 - 阴影")] public GameObject[] shadowExplosion;
    [Header("爆炸 - 小")] public GameObject[] smallExplosion;
    [Header("爆炸 - 灵魂")] public GameObject[] soulExplosion;
    [Header("爆炸 - 火花")] public GameObject[] sparkleExplosion;
    [Header("爆炸 - 突兀")] public GameObject[] spikyExplosion;
    [Header("爆炸 - 星星")] public GameObject[] starExplosion;
    [Header("爆炸 - 星星 - 核心")] public GameObject[] starIntenseExplosion;
    [Header("爆炸 - 风暴")] public GameObject[] stormExplosion;
}