using EpicToonFX;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Color = UnityEngine.Color;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Melee,
        Remote
    }

    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("边缘检测点")] public Transform edgeCheck;
    [Header("警备检测点")] public Transform detectedCheck;
    [Header("地面检测点")] public Transform groundCheck;
    [Header("近战攻击检测点")] public Transform meleeAttackCheck;
    [Header("远程攻击检测点")] public Transform remoteAttackCheck;
    [Header("实体数据")] public D_E_Base entityData;

    public GameObject aliveGobj { get; private set; } // 存活游戏对象
    public BoxCollider2D collider2d { get; private set; } // 碰撞体
    public Animator at { get; private set; } // 动画
    public Rigidbody2D rb { get; private set; } // 刚体
    public E_StateMachine stateMachine { get; private set; } // 状态机
    public AnimationToScript animationToScript { get; private set; } // 动画事件引用脚本
    public int knockbackDirection { get; private set; } // 眩晕击退方向 1右
    public GameObject stunEffect { get; private set; } // 眩晕特效

    private float attackEffectSpace = 5.0f;// 切换特效间隔
    public float lastAttackEffectTime { get; private set; } // 上次切换特效时间

    [HideInInspector] public bool isUseAbility2 = false; // 是否使用过技能2

    [HideInInspector] public MaterialPropertyBlock mpb; // 渲染材质空值
    [HideInInspector] public Renderer render;  // 渲染

    [HideInInspector] public Vector2 movement;// 刚体速度
    [HideInInspector] public int facingDirection = 1; // 面向方向 1右
    [HideInInspector] public int currentStunCount;// 当前距离击晕次数
    [HideInInspector] public float currentHealth; // 当前生命值
    [HideInInspector] public bool isStuning;//是否眩晕
    [HideInInspector] public bool isHurting;//是否受伤
    [HideInInspector] public bool isDead; // 是否死亡

    [HideInInspector] public GameObject ability2Effect1; // 技能2特效1
    [HideInInspector] public GameObject ability2Effect2;// 技能2特效2

    #region 状态

    public E_S_Dead dead; // 死亡
    public E_S_Idle idle; // 空闲
    public E_S_Move move; // 移动
    public E_S_Stun stun; // 眩晕
    public E_S_Hurt hurt; // 受伤
    public E_S_Dodge dodge; // 闪避
    public E_S_Detected detected; // 警备
    public E_S_FindPlayer findPlayer; // 寻找玩家
    public E_S_MeleeAttack meleeAttack; // 近战攻击
    public E_S_RemoteAttack remoteAttack; // 远程攻击
    public E_S_Ability1 ability1; // 技能1
    public E_S_Ability2 ability2;
    public E_S_Charge charge; // 追击

    [Header("死亡数据")] public D_E_Dead deadData;
    [Header("空闲数据")] public D_E_Idle idleData;
    [Header("移动数据")] public D_E_Move moveData;
    [Header("眩晕数据")] public D_E_Stun stunData;
    [Header("闪避数据")] public D_E_Dodge dodgeData;
    [Header("受伤数据")] public D_E_Hurt hurtData;
    [Header("警备数据")] public D_E_Detected detectedData;
    [Header("寻找玩家数据")] public D_E_FindPlayer findPlayerData;
    [Header("近战攻击数据")] public D_E_MeleeAttack meleeAttackData;
    [Header("远程攻击数据")] public D_E_RemoteAttack remoteAttackData;
    [Header("技能1数据")] public D_E_Ability1 ability1Data;
    [Header("技能2数据")] public D_E_Ability2 ability2Data;
    [Header("追击数据")] public D_E_Charge chargeData;

    #endregion 状态

    #region 其他函数

    // 接收伤害回调
    public virtual void AcceptPlayerDamage(AttackInfo attackInfo)
    {
        if (isDead || stateMachine.currentState == ability2) return;
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
        // 攻击特效
        EffectBox.Instance.CreateEffect(entityData.effectRes, aliveGobj.transform.position, aliveGobj.transform.rotation);

        // 判断是否处于眩晕中
        if (!isStuning)
        {
            currentStunCount--;
            if (currentStunCount > 0)
            {
                isHurting = true;
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

    // 更新动画
    private void UpdateAnimation()
    {
        at.SetFloat("yVelocity", rb.velocity.y);
    }

    // 更新攻击特效
    private void UpdateAttackEffect()
    {
        if (Time.time >= lastAttackEffectTime + attackEffectSpace)
        {
            lastAttackEffectTime = Time.time;
            ETFXFireProjectile.Instance.SwitchEnemyAttackEffect();
        }
    }

    // 判断是否有该游戏对象 赋值 然后禁用
    private bool JudGeGameObjAlive(string name)
    {
        return aliveGobj.transform.Find(name) != null;
    }

    // 初始化某些特效游戏对象
    private void InitEffect()
    {
        if (JudGeGameObjAlive("StunEffect"))
        {
            stunEffect = aliveGobj.transform.Find("StunEffect").gameObject;
            stunEffect.SetActive(false);
        }
        if (JudGeGameObjAlive("Ability2Effect"))
        {
            ability2Effect1 = aliveGobj.transform.Find("Ability2Effect").gameObject;
            ability2Effect1.SetActive(false);
        }
        if (JudGeGameObjAlive("Ability2BottomEffect"))
        {
            ability2Effect2 = aliveGobj.transform.Find("Ability2BottomEffect").gameObject;
            ability2Effect2.SetActive(false);
        }
    }

    #endregion 其他函数

    #region Unity生命周期

    public virtual void Start()
    {
        dead = new E_S_Dead(stateMachine, this, "dead", deadData);
        idle = new E_S_Idle(stateMachine, this, "idle", idleData);
        move = new E_S_Move(stateMachine, this, "move", moveData);
        stun = new E_S_Stun(stateMachine, this, "stun", stunData);
        hurt = new E_S_Hurt(stateMachine, this, "hurt", hurtData);
        dodge = new E_S_Dodge(stateMachine, this, "dodge", dodgeData);
        detected = new E_S_Detected(stateMachine, this, "detected", detectedData);
        findPlayer = new E_S_FindPlayer(stateMachine, this, "findPlayer", findPlayerData);
        meleeAttack = new E_S_MeleeAttack(stateMachine, this, "meleeAttack", meleeAttackCheck, meleeAttackData);
        remoteAttack = new E_S_RemoteAttack(stateMachine, this, "remoteAttack", remoteAttackCheck, remoteAttackData);
        ability1 = new E_S_Ability1(stateMachine, this, "ability1", ability1Data);
        ability2 = new E_S_Ability2(stateMachine, this, "ability2", remoteAttackCheck, remoteAttackData, ability2Data);
        charge = new E_S_Charge(stateMachine, this, "charge", chargeData);
        stateMachine.Init(move);
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
        lastAttackEffectTime = Time.time;

        stateMachine = new E_StateMachine();

        InitEffect();
    }

    public virtual void Update()
    {
        stateMachine.currentState.Update();
        CheckSwitchState();
        UpdateAnimation();
        UpdateAttackEffect();
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
        Gizmos.DrawWireSphere(meleeAttackCheck.transform.position, meleeAttackData.meleeAttackRadius);
    }

    #endregion Unity生命周期

    #region 设置

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

    // 设置转身
    public virtual void Turn()
    {
        facingDirection *= -1;
        aliveGobj.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    // 设置骨骼动画渲染透明度
    public virtual void SetSpineTransparent(float transparent)
    {
        Color color = new Color(1f, 1f, 1f, transparent);
        mpb.SetColor("_Color", color);
        render.SetPropertyBlock(mpb);
    }

    #endregion 设置

    #region 检测

    // 检测切换状态
    public void CheckSwitchState()
    {
        if (stateMachine.currentState == ability2) return;
        if (CheckDead())
        {
            stateMachine.ChangeState(dead);
        }
        else if (CheckAblity2() && !isUseAbility2)
        {
            stateMachine.ChangeState(ability2);
            isUseAbility2 = true;
        }
        else if (CheckStun())
        {
            stateMachine.ChangeState(stun);
        }
        else if (CheckHurt())
        {
            stateMachine.ChangeState(hurt);
        }
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

    // 检测是否可以闪避
    public bool CheckCanDodge()
    {
        if (Time.time >= dodge.endDodgeTime + dodge.dodgeData.cooldown)
        {
            return true;
        }
        return false;
    }

    // 检测死亡
    private bool CheckDead()
    {
        return isDead && stateMachine.currentState != dead;
    }

    // 检测眩晕
    private bool CheckStun()
    {
        return isStuning && stateMachine.currentState != stun && stateMachine.currentState != ability2;
    }

    // 检测受伤
    private bool CheckHurt()
    {
        return isHurting && stateMachine.currentState != hurt && Time.time >= hurt.startTime + hurtData.hurtCoolDown;
    }

    // 检测技能2
    private bool CheckAblity2()
    {
        return currentHealth <= 50.0f && entityData.enemyType == EnemyType.Remote;
    }

    #endregion 检测
}