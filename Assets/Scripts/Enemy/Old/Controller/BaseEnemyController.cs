using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Dead,
        Knockback
    }  // 敌人状态枚举

    private State currentState = State.Moving;   // 敌人当前状态

    /* 自身属性 */
    #region
    private GameObject aliveGobj; // 活着的敌人
    private Rigidbody2D rb; // 敌人刚体
    private Animator at;  // 敌人动画
    #endregion

    /* 射线检测 */
    #region
    [SerializeField][Header("检测层级")] public LayerMask checkLayer;
    [SerializeField][Header("地面检测点")] public Transform groundCheck;
    [SerializeField][Header("地面检测距离")] public float groundCheckDistance = 0.1f;
    [SerializeField][Header("墙壁检测点")] public Transform wallCheck;
    [SerializeField][Header("墙壁检测距离")] public float wallCheckDistance = 0.1f;
    private bool wallDetected; // (墙壁保护)是否将要触墙
    private bool groundDetected; // (地面保护)是否将要下坠
    #endregion

    /* 移动 */
    #region
    [SerializeField][Header("移动速度")] private float moveSpeed = 3.0f;
    private int facingDirection = 1; // 面向方向 1右
    private Vector2 movement; // 敌人当前刚体速度

    /* 受击 */
    [SerializeField][Header("最大生命值")] private float maxHealth = 100.0f;
    [SerializeField][Header("受击持续时间")] private float knockbackDuration;
    [SerializeField][Header("受击冲击速度")] private Vector2 knockbackSpeed;
    private float currentHealth; // 当前生命值
    private float startKnockbackTime; // 开始受击时间
    private int damageDirection; // 伤害来源方向 1右

    /* 接触 */
    [SerializeField][Header("接触检测点")] private Transform touchCheck;
    [SerializeField][Header("接触检测盒子大小")] private Vector2 touchCheckBox;
    [SerializeField][Header("接触检测层级")] private LayerMask checkTouchLayer;
    [SerializeField][Header("接触伤害")] private float touchDamage = 10.0f;
    private float lastTouchTime; //上一次接触时间
    private float touchCoolDown = 0.2f; // 接触冷却
    private float[] touchInfo = new float[2]; // 接触传递信息

    #endregion

    private void Awake()
    {
        aliveGobj = transform.Find("Alive").gameObject;
        rb = aliveGobj.GetComponent<Rigidbody2D>();
        at = aliveGobj.GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMoving();
                break;

            case State.Dead:
                UpdateDead();
                break;

            case State.Knockback:
                UpdateKnockback();
                break;

            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.transform.position, new Vector2(wallCheck.transform.position.x + wallCheckDistance, wallCheck.transform.position.y));
        Gizmos.DrawLine(groundCheck.transform.position, new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y - groundCheckDistance));
        Gizmos.DrawWireCube(touchCheck.position, touchCheckBox);
    }

    // 受到伤害回调
    public void AcceptPlayerDamage(AttackInfo attackInfo)
    {
        currentHealth -= attackInfo.damage;
        var pos = aliveGobj.transform.position;
        var rot = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        EffectBox.Instance.WildBoarHit(pos, rot);
        // 判断伤害方向
        if (aliveGobj.transform.position.x < attackInfo.damageSourcePosX)
        {
            // 伤害来源来右边
            damageDirection = 1;
        }
        else
        {
            damageDirection = -1;
        }

        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth < 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    // 检测接触
    private void CheckTouch()
    {
        if (Time.time >= lastTouchTime + touchCoolDown)
        {
            RaycastHit2D hit = Physics2D.BoxCast(touchCheck.position, touchCheckBox, 0.0f, Vector2.zero, 0, checkTouchLayer);
            if (hit)
            {
                lastTouchTime = Time.time;
                touchInfo[0] = touchDamage;
                touchInfo[1] = aliveGobj.transform.position.x;
                hit.collider.transform.SendMessage("AcceptTouchDamage", touchInfo);
            }
        }
    }

    // 转身
    private void Turn()
    {
        facingDirection *= -1;
        aliveGobj.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    // 切换敌人状态
    private void SwitchState(State state)
    {
        // 退出当前状态
        switch (currentState)
        {
            case State.Moving:
                ExitMoving();
                break;

            case State.Dead:
                ExitDead();
                break;

            case State.Knockback:
                ExitKnockback();
                break;

            default:
                break;
        }

        // 进入切换状态
        switch (state)
        {
            case State.Moving:
                EnterMoving();
                break;

            case State.Dead:
                EnterDead();
                break;

            case State.Knockback:
                EnterKnockback();
                break;

            default:
                break;
        }

        currentState = state;
    }

    // 移动
    #region

    private void EnterMoving()
    {
    }

    private void UpdateMoving()
    {
        wallDetected = Physics2D.Raycast(wallCheck.position, aliveGobj.transform.right, wallCheckDistance, checkLayer);
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, checkLayer);

        // 检测接触
        CheckTouch();

        // 检测到墙壁 或者 未检测到地面
        if (wallDetected || !groundDetected)
        {
            Turn();
        }
        else
        {
            movement.Set(moveSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;
        }
    }

    private void ExitMoving()
    {
    }

    #endregion

    //击退
    #region

    private void EnterKnockback()
    {
        startKnockbackTime = Time.time;
        movement.Set(knockbackSpeed.x * -damageDirection, knockbackSpeed.y);
        rb.velocity = movement;
        at.SetBool("knockBack", true);
    }

    private void UpdateKnockback()
    {
        if (Time.time > startKnockbackTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockback()
    {
        at.SetBool("knockBack", false);
    }

    #endregion

    //死亡
    #region

    private void EnterDead()
    {
        var pos = aliveGobj.transform.position;
        EffectBox.Instance.Chunk(pos);
        EffectBox.Instance.Blood(pos);
        Destroy(gameObject);
    }

    private void UpdateDead()
    {
    }

    private void ExitDead()
    {
    }

    #endregion
}