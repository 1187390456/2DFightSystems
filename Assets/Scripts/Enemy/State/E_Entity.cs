using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class E_Entity : MonoBehaviour
{
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("边缘检测点")] public Transform edgeCheck;
    [Header("警备检测点")] public Transform detectedCheck;
    [Header("近战攻击检测点")] public Transform meleeAttackCheck;
    [Header("实体数据")] public D_E_Base entityData;

    public GameObject aliveGobj { get; private set; } // 存活游戏对象
    public Animator at { get; private set; } // 动画
    public Rigidbody2D rb { get; private set; } // 刚体
    public E_StateMachine stateMachine { get; private set; } // 状态机
    public AnimationToScript animationToScript { get; private set; } // 动画事件引用脚本

    private Vector2 movement;// 刚体速度
    private int facingDirection; // 面向方向 1右

    private int knockbackStunDirection; // 眩晕击退方向 1右
    [HideInInspector] public bool canEnterStun { get; set; } //是否可以进入眩晕
    [HideInInspector] public bool isStuning { get; set; } //是否处于眩晕中
    [HideInInspector] public int currentStunCount { get; set; }  // 当前距离击晕次数

    [HideInInspector] public bool canEnterHurt { get; set; } //是否可以进入受伤状态
    [HideInInspector] public bool isHurting { get; set; } //是否受伤

    // 接收伤害回调
    public virtual void AcceptPlayerDamage(AttackInfo attackInfo)
    {
        if (aliveGobj.transform.position.x < attackInfo.damageSourcePosX)
        {
            knockbackStunDirection = -1;
        }
        else
        {
            knockbackStunDirection = 1;
        }
        if (!isStuning)
        {
            SetNoStunVelocity(knockbackStunDirection);
        }
        else
        {
            SetStunVelocity();
        }
    }

    public virtual void Awake()
    {
        aliveGobj = transform.Find("Alive").gameObject;
        rb = aliveGobj.GetComponent<Rigidbody2D>();
        at = aliveGobj.GetComponent<Animator>();
        animationToScript = aliveGobj.GetComponent<AnimationToScript>();

        facingDirection = 1;

        currentStunCount = entityData.stunCount;
        canEnterStun = false;
        isStuning = false;
        canEnterHurt = false;
        isHurting = false;

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
        Gizmos.DrawWireSphere(new Vector2(detectedCheck.position.x + entityData.minDetectedDistance * facingDirection, detectedCheck.position.y), 0.5f);
        Gizmos.DrawWireSphere(new Vector2(detectedCheck.position.x + entityData.maxDetectedDistance * facingDirection, detectedCheck.position.y), 0.5f);
    }

    // 设置刚体速度
    public virtual void SetVelocity(float moveSpeed)
    {
        movement.Set(moveSpeed * facingDirection, rb.velocity.y);
        rb.velocity = movement;
    }

    // 设置未眩晕刚体速度
    public virtual void SetNoStunVelocity(int knockbackStunDirection)
    {
        currentStunCount--;
        if (currentStunCount > 0)
        {
            movement.Set(rb.velocity.x, entityData.knockbackSpeed.y);
            rb.velocity = movement;
            canEnterHurt = true;
        }
        else
        {
            movement.Set(entityData.knockbackSpeed.x * knockbackStunDirection, entityData.knockbackSpeed.y);
            rb.velocity = movement;
            canEnterStun = true;
        }
    }

    // 设置眩晕刚体速度
    public virtual void SetStunVelocity()
    {
        movement.Set(rb.velocity.x, entityData.knockbackSpeed.y);
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
}