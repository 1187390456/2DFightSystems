﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public static PlayerAttackController Instance; //单例
    public bool isAttacking;  // 是否正在攻击

    private Animator at; // 自身动画
    private Rigidbody2D rb; // 自身刚体

    [SerializeField] [Header("有效攻击输入时间")] private float inputTime = 0.2f;
    [SerializeField] [Header("攻击1造成的伤害")] private float attack1Damage = 10;
    [SerializeField] [Header("攻击检测点")] private Transform attackCheck;
    [SerializeField] [Header("攻击检测半径")] private float attackCheckRadius;
    [SerializeField] [Header("可被攻击层级")] private LayerMask CanBeAttack;

    private bool canAttack = true; // 是否能够攻击
    private bool attack1Switch = true; // 攻击1切换
    private bool isAttackInputing; // 是否正在攻击输入
    private float lastInpuTime; // 上次攻击输入时间

    private void Awake()
    {
        Instance = this;
        at = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        at.SetBool("canAttack", canAttack);
    }

    private void Update()
    {
        CheckInput();
        ChechAttack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    // 攻击1 动画结束回调
    public void AnimationAttack1Done()
    {
        isAttacking = false;
        at.SetBool("isAttacking", isAttacking);
        at.SetBool("attack1", false);
    }

    // 攻击1 碰撞检测回调
    public void AnimationAttack1Hit()
    {
        Collider2D[] objs = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius, CanBeAttack);
        AttackInfo attackInfo = new AttackInfo()
        {
            damage = attack1Damage,
            damageSourcePosX = transform.position.x
        };
        foreach (var item in objs)
        {
            item.transform.parent.SendMessage("AcceptPlayerDamage", attackInfo);
        }
    }

    // 开始攻击
    public void StartAttack()
    {
        if (canAttack)
        {
            isAttackInputing = true;
            lastInpuTime = Time.time;
        }
    }

    // 检测输入
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
        {
            StartAttack();
        }
    }

    // 检测攻击
    private void ChechAttack()
    {
        if (isAttackInputing)
        {
            if (!isAttacking)
            {
                isAttackInputing = false;
                isAttacking = true;
                attack1Switch = !attack1Switch;
                at.SetBool("attack1", true);
                at.SetBool("isAttacking", isAttacking);
                at.SetBool("attack1Switch", attack1Switch);
            }
        }
        if (Time.time >= lastInpuTime + inputTime)
        {
            isAttackInputing = false;
        }
    }
}