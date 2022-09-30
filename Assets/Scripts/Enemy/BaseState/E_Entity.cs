using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Color = UnityEngine.Color;

public class E_Entity : MonoBehaviour
{
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("边缘检测点")] public Transform edgeCheck;
    [Header("警备检测点")] public Transform detectedCheck;
    [Header("地面检测点")] public Transform groundCheck;
    [Header("近战攻击检测点")] public Transform meleeAttackCheck;
    [Header("实体数据")] public D_E_Base entityData;

    public GameObject aliveGobj { get; private set; } // 存活游戏对象
    public BoxCollider2D collider2d { get; private set; } // 碰撞体
    public Animator at { get; private set; } // 动画
    public Rigidbody2D rb { get; private set; } // 刚体
    public E_StateMachine stateMachine { get; private set; } // 状态机
    public AnimationToScript animationToScript { get; private set; } // 动画事件引用脚本
    public int knockbackDirection { get; private set; } // 眩晕击退方向 1右

    [HideInInspector] public MaterialPropertyBlock mpb; // 渲染材质空值
    [HideInInspector] public Renderer render;  // 渲染

    [HideInInspector] public Vector2 movement;// 刚体速度
    [HideInInspector] public int facingDirection = 1; // 面向方向 1右
    [HideInInspector] public int currentStunCount;// 当前距离击晕次数
    [HideInInspector] public float currentHealth; // 当前生命值
    [HideInInspector] public bool isStuning;//是否眩晕
    [HideInInspector] public bool isHurting;//是否受伤
    [HideInInspector] public bool isDead; // 是否死亡

    // 接收伤害回调
    public virtual void AcceptPlayerDamage(AttackInfo attackInfo)
    {
        if (isDead) return;
        currentHealth -= attackInfo.damage;

        // 判断死亡
        if (currentHealth <= 0)
        {
            isDead = true;
        }
        // 判断受击方向
        if (aliveGobj.transform.position.x < attackInfo.damageSourcePosX)
        {
            knockbackDirection = -1;
        }
        else
        {
            knockbackDirection = 1;
        }
        // 能眩晕 一般怪物
        if (entityData.canStun)
        {
            var rot = Quaternion.Euler(new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            EffectBox.Instance.CreateEffect(entityData.effectRes, aliveGobj.transform.position, rot);
            // 判断是否处于眩晕中
            if (!isStuning)
            {
                currentStunCount--;
                if (currentStunCount > 0)
                {
                    isHurting = true;
                    SetVelocity(entityData.knockbackSpeed, entityData.knockbackAngle, knockbackDirection);
                }
                else
                {
                    isStuning = true;
                }
            }
            // 眩晕中接地
            else if (CheckGround())
            {
                SetVelocityY(entityData.stunKnockbackSpeedY);
            }
        }
        // 霸体怪物
        else
        {
            if (CheckGround())
            {
                SetVelocity(entityData.knockbackSpeed, entityData.knockbackAngle, knockbackDirection);
            }
            EffectBox.Instance.CreateEffect(entityData.effectRes, aliveGobj.transform.position, aliveGobj.transform.rotation);
        }
    }

    public virtual void Awake()
    {
        aliveGobj = transform.Find("Alive").gameObject;
        rb = aliveGobj.GetComponent<Rigidbody2D>();
        at = aliveGobj.GetComponent<Animator>();
        collider2d = aliveGobj.GetComponent<BoxCollider2D>();
        mpb = new MaterialPropertyBlock();
        render = aliveGobj.GetComponent<Renderer>();
        animationToScript = aliveGobj.GetComponent<AnimationToScript>();

        currentStunCount = entityData.stunCount;
        currentHealth = entityData.maxHealth;

        stateMachine = new E_StateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.Update();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.FixUpdate();
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + entityData.wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(edgeCheck.position, new Vector2(edgeCheck.position.x, edgeCheck.position.y - entityData.edgeCheckDistance));
        Gizmos.DrawLine(detectedCheck.position, new Vector2(detectedCheck.position.x + entityData.canMeleeDistance, detectedCheck.position.y));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - entityData.groundCheckDistance));
        Gizmos.DrawWireSphere(new Vector2(detectedCheck.position.x + entityData.minDetectedDistance * facingDirection, detectedCheck.position.y), 0.5f);
        Gizmos.DrawWireSphere(new Vector2(detectedCheck.position.x + entityData.maxDetectedDistance * facingDirection, detectedCheck.position.y), 0.5f);
    }

    // 设置X轴刚体速度
    public virtual void SetVelocityX(float velocity)
    {
        movement.Set(velocity * facingDirection, rb.velocity.y);
        rb.velocity = movement;
    }

    // 设置Y轴刚体速度
    public virtual void SetVelocityY(float velocity)
    {
        movement.Set(rb.velocity.x, velocity);
        rb.velocity = movement;
    }

    // 设置具体刚体速度 float
    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        movement.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = movement;
    }

    // 设置具体刚体速度 V2
    public virtual void SetVelocity(Vector2 velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        movement.Set(angle.x * velocity.x * direction, angle.y * velocity.y);
        rb.velocity = movement;
    }

    // 设置转身
    public virtual void Turn()
    {
        facingDirection *= -1;
        aliveGobj.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    // 墙壁检测
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGobj.transform.right, entityData.wallCheckDistance, LayerMask.GetMask("Ground"));
    }

    // 地面检测
    public virtual bool CheckGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckDistance, LayerMask.GetMask("Ground"));
    }

    // 边缘检测
    public virtual bool CheckEdge()
    {
        return Physics2D.Raycast(edgeCheck.position, Vector2.down, entityData.edgeCheckDistance, LayerMask.GetMask("Ground"));
    }

    // 最大警备点检测
    public virtual bool CheckMaxDetected()
    {
        return Physics2D.Raycast(detectedCheck.position, aliveGobj.transform.right, entityData.maxDetectedDistance, LayerMask.GetMask("Player"));
    }

    // 最小警备点检测
    public virtual bool CheckMinDetected()
    {
        return Physics2D.Raycast(detectedCheck.position, aliveGobj.transform.right, entityData.minDetectedDistance, LayerMask.GetMask("Player"));
    }

    // 保护条件
    public virtual bool IsProtect()
    {
        return !CheckEdge() || CheckWall();
    }

    // 近战攻击检测
    public virtual bool IsReachCanMeleeAttack()
    {
        return Physics2D.Raycast(detectedCheck.position, aliveGobj.transform.right, entityData.canMeleeDistance, LayerMask.GetMask("Player"));
    }

    // 设置骨骼动画渲染透明度
    public virtual void SetSpineTransparent(float transparent)
    {
        Color color = new Color(1f, 1f, 1f, transparent);
        mpb.SetColor("_Color", color);
        render.SetPropertyBlock(mpb);
    }
}