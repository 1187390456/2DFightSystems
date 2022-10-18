using EpicToonFX;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.EventSystems.EventTrigger;
using Color = UnityEngine.Color;

public class Enemy : MonoBehaviour, IDamageable
{
    #region 核心

    public Core core { get; private set; }
    public Movement movement => core.movement;
    public EnemyCollisionSenses sense => core.enemyCollisionSenses;
    public EnemyState state => core.enemyState;

    #endregion 核心

    public D_E_Base entityData => state.entityData;
    public E_StateMachine stateMachine => state.stateMachine;

    public enum EnemyType
    {
        Melee,
        Remote
    }

    public Animator at { get; private set; } // 动画

    public AnimationToScript animationToScript { get; private set; } // 动画事件引用脚本
    public int knockbackDirection { get; private set; } // 眩晕击退方向 1右
    public GameObject stunEffect { get; private set; } // 眩晕特效

    private float attackEffectSpace = 5.0f;// 切换特效间隔
    public float lastAttackEffectTime { get; private set; } // 上次切换特效时间

    [HideInInspector] public bool isUseAbility2 = false; // 是否使用过技能2

    [HideInInspector] public MaterialPropertyBlock mpb; // 渲染材质空值
    [HideInInspector] public Renderer render;  // 渲染

    [HideInInspector] public int currentStunCount;// 当前距离击晕次数
    [HideInInspector] public float currentHealth; // 当前生命值
    [HideInInspector] public bool isStuning;//是否眩晕
    [HideInInspector] public bool isHurting;//是否受伤
    [HideInInspector] public bool isDead; // 是否死亡

    [HideInInspector] public GameObject ability2Effect1; // 技能2特效1
    [HideInInspector] public GameObject ability2Effect2;// 技能2特效2

    #region 其他函数

    // 接收伤害回调
    public virtual void AcceptPlayerDamage(AttackInfo attackInfo)
    {
        if (isDead || stateMachine.currentState == state.ability2) return;
        currentHealth -= attackInfo.damage;

        // 判断死亡
        if (currentHealth <= 0)
        {
            isDead = true;
        }
        // 判断受击方向
        if (transform.position.x < attackInfo.damageSourcePosX)
        {
            knockbackDirection = -1;
        }
        else
        {
            knockbackDirection = 1;
        }
        // 攻击特效
        EffectBox.Instance.CreateEffect(entityData.effectRes, transform.position, transform.rotation);

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
        else if (sense.Ground())
        {
            movement.SetVelocityY(entityData.stunKnockbackSpeedY);
        }
    }

    // 更新动画
    private void UpdateAnimation()
    {
        at.SetFloat("yVelocity", movement.rbY);
    }

    // 更新攻击特效
    private void UpdateAttackEffect()
    {
        if (Time.time >= lastAttackEffectTime + attackEffectSpace)
        {
            lastAttackEffectTime = Time.time;
            if (ETFXFireProjectile.Instance)
            {
                ETFXFireProjectile.Instance.SwitchEnemyAttackEffect();
            }
        }
    }

    // 判断是否有该游戏对象 赋值 然后禁用
    private bool JudGeGameObjAlive(string name)
    {
        return transform.Find(name) != null;
    }

    // 初始化某些特效游戏对象
    private void InitEffect()
    {
        if (JudGeGameObjAlive("StunEffect"))
        {
            stunEffect = transform.Find("StunEffect").gameObject;
            stunEffect.SetActive(false);
        }
        if (JudGeGameObjAlive("Ability2Effect"))
        {
            ability2Effect1 = transform.Find("Ability2Effect").gameObject;
            ability2Effect1.SetActive(false);
        }
        if (JudGeGameObjAlive("Ability2BottomEffect"))
        {
            ability2Effect2 = transform.Find("Ability2BottomEffect").gameObject;
            ability2Effect2.SetActive(false);
        }
    }

    #endregion 其他函数

    #region Unity生命周期

    public virtual void Awake()
    {
        core = GetComponentInChildren<Core>();

        at = GetComponent<Animator>();
        mpb = new MaterialPropertyBlock();
        render = GetComponent<Renderer>();
        animationToScript = GetComponent<AnimationToScript>();

        InitEffect();
    }

    private void Start()
    {
        currentStunCount = entityData.stunCount;
        currentHealth = entityData.maxHealth;
        lastAttackEffectTime = Time.time;
    }

    public virtual void Update()
    {
        CheckSwitchState();
        UpdateAnimation();
        UpdateAttackEffect();
    }

    #endregion Unity生命周期

    #region 设置

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
        if (stateMachine.currentState == state.dead) return;
        if (CheckDead())
        {
            stateMachine.ChangeState(state.dead);
        }
        if (stateMachine.currentState == state.ability2) return;
        else if (CheckAblity2() && !isUseAbility2)
        {
            stateMachine.ChangeState(state.ability2);
            isUseAbility2 = true;
        }
        else if (CheckStun())
        {
            stateMachine.ChangeState(state.stun);
        }
        else if (CheckHurt())
        {
            stateMachine.ChangeState(state.hurt);
        }
    }

    // 保护条件
    public virtual bool IsProtect() => !sense.Edge() || sense.Wall();

    // 检测是否可以闪避
    public bool CheckCanDodge() => Time.time >= state.dodge.endDodgeTime + state.dodge.dodgeData.cooldown;

    // 检测死亡
    private bool CheckDead() => isDead && stateMachine.currentState != state.dead;

    // 检测眩晕
    private bool CheckStun() => isStuning && stateMachine.currentState != state.stun && stateMachine.currentState != state.ability2 && stateMachine.currentState != state.dead;

    // 检测受伤
    private bool CheckHurt() => isHurting && stateMachine.currentState != state.hurt && Time.time >= state.hurt.startTime + state.hurtData.hurtCoolDown;

    // 检测技能2
    private bool CheckAblity2() => currentHealth <= 50.0f && entityData.enemyType == EnemyType.Remote;

    #endregion 检测
}