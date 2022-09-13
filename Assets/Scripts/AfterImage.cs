using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer afterImageSR; // 残影精灵图渲染
    private Transform player; //玩家
    private SpriteRenderer playerSR; // 玩家精灵图渲染

    private float alpha; // 透明度
    private float startAlpha = 0.8f; // 初始透明度
    private float alphaMultiplier = 0.99f; // 透明乘数
    private float existTime = 0.99f; // 存在时间
    private Color color; // 渲染颜色
    private float activeTime; // 激活时间

    private void Awake()
    {
        afterImageSR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();
    }
    // 激活初始状态
    private void OnEnable()
    {
        afterImageSR.sprite = playerSR.sprite;
        alpha = startAlpha;
        transform.position = player.position;
        transform.rotation = player.rotation;
        activeTime = Time.time;
    }
    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1, 1, 1, alpha);
        afterImageSR.color = color;

        // 过了存在时间
        if (Time.time >= (activeTime + existTime))
        {
            ObjectPool.Instance.AddPool(gameObject);
        }
    }
}
