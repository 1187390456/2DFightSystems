using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    bool combatEnabled;//启用战斗

    [SerializeField]
    float inputTimer;//输入定时器

    [SerializeField]
    float attack1Radius;//命中框的物理2D圆的半径

    [SerializeField]
    float attack1Damage;//攻击一次伤害

    [SerializeField]
    private float stunDamageAmount = 1f; //眩晕伤害量

    [SerializeField]
    Transform attack1HitBoxPos;//攻击命中框位置

    [SerializeField]
    LayerMask whatIsDamageable;//什么是可损坏的

    bool gotInput;//获取输入
    bool isAttacking;//攻击
    bool isFirstAttack;//第一次攻击

    float lastInputTime = Mathf.NegativeInfinity;//最后输入时间

    AttackDetails attackDetails;//攻击的细节

    Animator anim;//动画

    Player PC;
    PlayerStats PS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<Player>();
        PS = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }
    //检查战斗输入
    void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                //战斗
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }
    //检查攻击
    void CheckAttacks()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }
        if (Time.time >= lastInputTime + inputTimer)
        {
            //等待新的输入
            gotInput = false;
        }
    }
    //检查攻击击中判定体积
    void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails.damageAmount = attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = stunDamageAmount;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
            //实例化

        }
    }
    //完成攻击1
    void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }
    //伤害
    void Damage(AttackDetails attackDetails)
    {
        if (!PC.GetDashStatus())
        {
            int direction;

            PS.DecreasHealth(attackDetails.damageAmount);

            if (attackDetails.position.x < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            PC.Knockback(direction);
        }
    }
    private void OnDrawGizmos()
    {
        //绘制命中框
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
