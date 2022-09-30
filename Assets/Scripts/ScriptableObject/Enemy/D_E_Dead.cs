using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dead", menuName = "Data/Enemy Data/State Data/Dead Data")]
public class D_E_Dead : ScriptableObject
{
    [Header("是否可以重生")] public bool canRebirth = false;
    [Header("重生时间")] public float rebirthTime = 4.0f;
    [Header("是否是霸体怪物 (Monster) ")] public bool isMonster = true;
    [Header("透明帧间隔 (Monster)")] public float transparentSpace = 1.0f;
    [Header("死亡特效数组 (Monster)")] public List<GameObject> effectListRes = new List<GameObject>();
    [Header("死亡特效位置偏移 (Monster)")] public Vector2 effectOffset = new Vector2(0.0f, 1.0f);
}