using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    Color color;//颜色
    private Transform player;//玩家
    private SpriteRenderer playerSR;//玩家精灵图渲染器
    private SpriteRenderer afterSR;//残影精灵图渲染器
    private float startTime;//残影开始时间
    private float A;//透明度

    [Header("残影停留时间")] public float stopTime;
    [Header("开始透明度")] public float startA;
    [Header("透明度帧修复")] public int aFix;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();
        afterSR = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        transform.position = player.position;
        transform.rotation = player.rotation;
        afterSR.sprite = playerSR.sprite;
        startTime = Time.time;
        A = startA;
    }
    private void Update()
    {
        A -= aFix * Time.deltaTime;
        color = new Color(1, 1, 1, A);
        afterSR.color = color;
        if (Time.time >= startTime + stopTime)
        {
            ObjectPool.Instance.AddPool(gameObject);
        }
    }
}
