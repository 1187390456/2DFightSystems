using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class E_Entity : MonoBehaviour
{
    public E_StateMachine stateMachine = new E_StateMachine();//状态机
    public Animator anim { get; private set; }//动画
    public Rigidbody2D rb { get; private set; }//刚体
    public Vector2 movement;//速度
    public GameObject aliveGobj { get; private set; }//存活对象
    public Transform ground;//地面
    public Transform wall;//地面
    public Transform player1;//玩家
    public Transform player2;//玩家
    public bool isTouchGround;//是否接触地面
    public bool isTouchWall;//是否接触地面
    public bool isCheckPlayer1;//是否检查到玩家
    public bool isCheckPlayer2;//是否检查到玩家
    public float distance = 3.0f;//检测距离
    public float radius = 0.52f;//检测半径
    public int facingInt = 1;
    public virtual void Awake()
    {
        aliveGobj = transform.Find("Alive").gameObject;
        anim = aliveGobj.GetComponent<Animator>();
        rb = aliveGobj.GetComponent<Rigidbody2D>();
    }
    public virtual void Start()
    {

    }
    public virtual void Update()
    {
        stateMachine.currentState.Update();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ground.position, new Vector2(ground.position.x, ground.position.y - distance));
        Gizmos.DrawLine(wall.position, new Vector2(wall.position.x + distance * facingInt, wall.position.y));
        Gizmos.DrawLine(player1.position, new Vector2(player1.position.x + 7f * facingInt, wall.position.y));
        Gizmos.DrawWireSphere(player2.position, radius);
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
        CheckEnvironment();
    }
    // 修改x轴刚体速度
    public virtual void SetVelocityX(float velocity)
    {
        movement.Set(velocity * facingInt, rb.velocity.y);
        rb.velocity = movement;
    }
    //检查环境
    public virtual void CheckEnvironment()
    {
        isTouchGround = Physics2D.Raycast(ground.position, new Vector2(0, -1), distance, LayerMask.GetMask("Ground"));
        isTouchWall = Physics2D.Raycast(wall.position, new Vector2(facingInt, 0), distance, LayerMask.GetMask("Ground"));
        isCheckPlayer1 = Physics2D.Raycast(player1.position, new Vector2(facingInt, 0), 7f, LayerMask.GetMask("Player"));
        isCheckPlayer2 = Physics2D.OverlapCircle(player2.position, radius, LayerMask.GetMask("Player"));
    }
    //转向
    public virtual void Turn()
    {
        facingInt *= -1;
        aliveGobj.transform.localScale = new Vector3(facingInt, 1, 1);
    }
}
