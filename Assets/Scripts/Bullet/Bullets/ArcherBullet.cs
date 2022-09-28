using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBullet : MonoBehaviour
{
    [Header("接触位置")] public Transform touchPos;
    [Header("接触半径")] public float touchradius = 0.17f;
    [Header("最大存在时间")] public float maxAliveTime = 3.0f;

    private AttackInfo attackInfo; // 攻击信息
    private Rigidbody2D rb; // 刚体

    private float xStartPos; // 起始位置

    private float speed; // 速度
    private float distance; // 飞行距离
    private float gravityScale; // 重力大小
    private float damage; // 攻击伤害

    private bool openGravity; // 是否开启重力
    private bool isTouchGround; // 是否触地

    private float startTime; // 开始时间

    private SpriteRenderer spriteRenderer; // 渲染精灵
    private float currentTrasparent = 1.0f; // 起始透明度
    private float trasparentSpace = 1.0f; //  透明减小帧间隔

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;
        xStartPos = transform.position.x;
        openGravity = false;
        startTime = Time.time;
    }

    private void Update()
    {
        CheckOffsetAngle();
        CheckOverTime();
    }

    // 检测开始偏移角度
    private void CheckOffsetAngle()
    {
        if (!isTouchGround && openGravity)
        {
            // 以刚体速度计算tan弧度 然后转成角度 向前进行生成rot
            var angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // 检测是否超过指定存在时间
    private void CheckOverTime()
    {
        if (Time.time >= startTime + maxAliveTime)
        {
            currentTrasparent -= trasparentSpace * Time.deltaTime;
            Color color = new Color(1, 1, 1, currentTrasparent);
            spriteRenderer.color = color;
            if (currentTrasparent <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isTouchGround)
        {
            CheckTouchPlayer();
            CheckTouchGround();
            CheckOverDistance();
        }
    }

    // 检测是否超过指定距离
    private void CheckOverDistance()
    {
        if (Mathf.Abs(xStartPos - transform.position.x) >= distance && !openGravity)
        {
            openGravity = true;
            rb.gravityScale = gravityScale;
        }
    }

    // 检测是否接触玩家
    private void CheckTouchPlayer()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(touchPos.position, touchradius, LayerMask.GetMask("Player"));
        if (hitPlayer)
        {
            attackInfo.damage = damage;
            attackInfo.damageSourcePosX = transform.position.x;
            hitPlayer.transform.SendMessage("AcceptAttackDamage", attackInfo);
            Destroy(gameObject);
        }
    }

    // 检测是否触地
    private void CheckTouchGround()
    {
        Collider2D hitGround = Physics2D.OverlapCircle(touchPos.position, touchradius, LayerMask.GetMask("Ground"));

        if (hitGround)
        {
            isTouchGround = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0.0f;
        }
    }

    public void AcceptParamas(float speed, float distance, float gravityScale, float damage)
    {
        this.speed = speed;
        this.distance = distance;
        this.gravityScale = gravityScale;
        this.damage = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(touchPos.position, touchradius);
    }
}