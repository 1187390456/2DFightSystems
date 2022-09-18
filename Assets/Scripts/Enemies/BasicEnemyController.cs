using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    enum State
    {
        Moving,
        Knockback,
        Dead
    }

    State currentState;//当前状态

    [SerializeField]
    float groundCheckDistance,//地面距离
        wallCheckDistance,//墙面距离
        movementSpeed,//运动速度
        maxHealth,//最大生命值
        knockbackDuration,//击退持续时间
        lastTouchDamageTime,//最后一次接触伤害时间
        touchDamageCooldown,//接触伤害冷却
        touchDamage,//接触伤害
        touchDamageWidth,//接触伤害宽度
        touchDamageHeight;//接触伤害高度

    [SerializeField]
    Transform
        groundCheck,//地面检查
        wallCheck,//墙面检查
        touchDamageCheck;//触摸伤害检查

    [SerializeField]
    LayerMask
        whatIsGround,
        whatIsPlayer;

    [SerializeField]
    Vector2 knockbackSpeed;//击退速度

    [SerializeField]
    GameObject
        hitParticle,//被击中粒子
        deathChunkParticle,//死亡块粒子
        deathBloodParticle;//血液粒子
    int
        facingDirection,//面向方向
        damageDirection;//损坏方向

    float
        currentHealth,//当前生命值
        knockbackStartTime;//击退开始时间
    float[] attackDetails = new float[2];//添加细节

    bool
        groundDetected,//地面监测
        wallDetected;//墙面检测

    GameObject alive;
    Rigidbody2D aliveRb;
    Animator aliveAnim;
    Vector2
        movement,
        touchDamageBotLeft,//左下角
        touchDamageTopRight;//右上角

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        currentHealth = maxHealth;
        facingDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }


    /*-----------移动状态---------*/

    //进入移动状态
    void EnterMovingState()
    {

    }
    //更新移动状态
    void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)//没有检测到地面和墙面
        {
            //翻转
            Flip();
        }
        else
        {
            //移动
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }
    //退出移动状态
    void ExitMovingState()
    {

    }

    /*-----------击退状态---------*/

    //进入击退状态
    void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }
    //更新击退状态
    void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }
    //退出击退状态
    void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    /*-----------死亡状态---------*/

    //进入死亡状态
    void EnterDeadState()
    {
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
    //更新死亡状态
    void UpdateDeadState()
    {

    }
    //退出死亡状态
    void ExitDeadState()
    {

    }

    /*-----------其他函数---------*/
    //损伤
    void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }
    //转向
    void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    //切换状态

    void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }
    //检查触摸伤害
    void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }
}
