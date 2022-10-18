using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePersonController : MonoBehaviour, IDamageable
{
    [SerializeField] [Header("最大生命值")] private float maxHealth = 9999;
    [SerializeField] [Header("是否启用受击状态")] private bool canKnockback;
    [SerializeField] [Header("受击冲击速度")] private Vector2 knockbackSpeed;
    [SerializeField] [Header("死亡时头部冲击速度")] private Vector2 dieTopBeKnockSpeed;
    [SerializeField] [Header("死亡时头部旋转力大小")] private float dieTopTorque;
    [SerializeField] [Header("受击时间")] private float beHitTime;

    private GameObject aliveGobj, topBodyGobj, botBodyGobj; // 游戏对象
    private Rigidbody2D aliveRg, topRg, botRg; // 刚体
    private Animator at; // 动画
    private float currentHealth; // 当前生命值
    private bool isBeHiting; // 是否受击中
    private float lastBeHitTime; // 上次受击时间点

    private bool isHitFromLeft;// 受击方向

    private int knockBackDirection
    {
        get
        {
            if (isHitFromLeft)
            {
                return 1;
            }
            return -1;
        }
    }// 击退方向

    private void Awake()

    {
        aliveGobj = transform.Find("Alive").gameObject;
        topBodyGobj = transform.Find("TopBroken").gameObject;
        botBodyGobj = transform.Find("BottomBroken").gameObject;

        at = aliveGobj.GetComponent<Animator>();

        aliveRg = aliveGobj.GetComponent<Rigidbody2D>();
        topRg = topBodyGobj.GetComponent<Rigidbody2D>();
        botRg = botBodyGobj.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        StaticWays.SetGameObjectActive(false, topBodyGobj, botBodyGobj);
    }

    private void Update()
    {
        CheckKnockbackStatu();
    }

    private void OnDestory()
    {
    }

    // 受到伤害回调
    public void AcceptPlayerDamage(AttackInfo attackInfo)
    {
        currentHealth -= attackInfo.damage;
        at.SetTrigger("damage");
        if (attackInfo.damageSourcePosX > transform.position.x)
        {
            isHitFromLeft = false;
        }
        else
        {
            isHitFromLeft = true;
        }
        at.SetBool("isHitFromLeft", isHitFromLeft);
        var pos = at.transform.position;
        var rot = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        EffectBox.Instance.FakePersonHit(pos, rot);
        if (canKnockback && currentHealth > 0.0f)
        {
            Knockback();
        }
        if (currentHealth <= 0.0f)
        {
            Died();
        }
    }

    // 检测受击状态
    private void CheckKnockbackStatu()
    {
        // 过了受击时间
        if (Time.time >= lastBeHitTime + beHitTime && isBeHiting)
        {
            isBeHiting = false;
            aliveRg.velocity = new Vector2(0.0f, aliveRg.velocity.y);
        }
    }

    // 击退
    private void Knockback()
    {
        if (!isBeHiting)
        {
            isBeHiting = true;
            lastBeHitTime = Time.time;
            aliveRg.velocity = new Vector2(knockbackSpeed.x * knockBackDirection, knockbackSpeed.y);
        }
    }

    // 死亡
    private void Died()
    {
        StaticWays.SetGameObjectActive(false, aliveGobj);
        StaticWays.SetGameObjectActive(true, topBodyGobj, botBodyGobj);
        StaticWays.AssignmentEach(topBodyGobj, aliveGobj, "Position");
        StaticWays.AssignmentEach(botBodyGobj, aliveGobj, "Position");

        botRg.velocity = new Vector2(knockbackSpeed.x * knockBackDirection, knockbackSpeed.y);
        topRg.velocity = new Vector2(dieTopBeKnockSpeed.x * knockBackDirection, dieTopBeKnockSpeed.y);
        // 施加旋转力 左手定则 正值左边 负值右边
        topRg.AddTorque(dieTopTorque * -knockBackDirection, ForceMode2D.Impulse);
    }
}