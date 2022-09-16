using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    private SpriteRenderer afterImageSR; // 自身精灵渲染
    private SpriteRenderer playerSR; // 玩家精灵渲染
    private Transform player; // 玩家
    private float alpha; // 透明度
    private float startAlpha = 0.8f; // 初始透明度
    private float alphaDecay = 2.0f; // 透明度固定帧率
    private float existTime = 0.99f; // 残影存在时间
    private float activeTime; // 残影激活时间
    private Color color; // 残影颜色声明

    private void Awake()
    {
        afterImageSR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();
    }

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
        alpha -= alphaDecay * Time.deltaTime;
        color = new Color(1, 1, 1, alpha);
        afterImageSR.color = color;

        // 过了存在时间
        if (Time.time >= (activeTime + existTime))
        {
            ObjectPool.Instance.AddPool(gameObject);
        }
    }
}