using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpineTransAction : MonoBehaviour
{
    [Header("数据资源列表")] public List<SkeletonDataAsset> dataList = new List<SkeletonDataAsset>();
    private Enemy enemy; // 当前脚本
    private SkeletonMecanim spineScript; // 骨骼控制脚本

    private float lastChangeTime; // 上次变换时间
    private float changeTimeSpace = 5.0f; // 变换时间间隔

    private void Awake()
    {
        spineScript = GetComponent<SkeletonMecanim>();
        enemy = GetComponent<Enemy>();
        spineScript.skeletonDataAsset = dataList[0]; // 默认第一个数据
        spineScript.initialSkinName = "V1"; // 初始皮肤
        spineScript.Initialize(true);
        lastChangeTime = Time.time;
    }

    private void Update()
    {
        if (enemy.stateMachine.currentState != enemy.state.dead && Time.time >= lastChangeTime + changeTimeSpace)
        {
            ChangeShape();
            lastChangeTime = Time.time;
        }
    }

    // 切换形态
    private void ChangeShape()
    {
        var index = Random.Range(0, dataList.Count); // 随机切换
        switch (index)
        {
            case 0:
                spineScript.skeletonDataAsset = dataList[0];
                var name = Random.Range(1, dataList.Count);
                spineScript.initialSkinName = $"V{name}";
                spineScript.Initialize(true);
                break;

            case 1:
                spineScript.skeletonDataAsset = dataList[1];
                spineScript.initialSkinName = "V2";
                spineScript.Initialize(true);
                break;

            case 2:
                spineScript.skeletonDataAsset = dataList[2];
                spineScript.initialSkinName = "V4";
                spineScript.Initialize(true);
                break;

            case 3:
                spineScript.skeletonDataAsset = dataList[3];
                spineScript.initialSkinName = "V3";
                spineScript.Initialize(true);
                break;

            case 4:
                spineScript.skeletonDataAsset = dataList[4];
                spineScript.initialSkinName = "V5";
                spineScript.Initialize(true);
                break;

            default:
                break;
        }
    }
}