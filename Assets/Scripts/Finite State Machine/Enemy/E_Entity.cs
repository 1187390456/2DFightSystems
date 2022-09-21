using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Entity : MonoBehaviour
{
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("边缘检测点")] public Transform edgeCheck;
    [Header("警备检测点")] public Transform detectedCheck;
    [Header("实体数据")] public D_E_Base entityData;

    public GameObject aliveGobj { get; private set; } // 存活游戏对象
    public Animator at { get; private set; } // 动画
    public Rigidbody2D rb { get; private set; } // 刚体
    public E_StateMachine stateMachine { get; private set; } // 状态机

    private Vector2 movement;// 刚体速度
    private int facingDirection; // 面向方向 1右

    public virtual void Awake()
    {
        aliveGobj = transform.Find("Alive").gameObject;
        rb = aliveGobj.GetComponent<Rigidbody2D>();
        at = aliveGobj.GetComponent<Animator>();

        facingDirection = 1;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + entityData.wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(edgeCheck.position, new Vector2(edgeCheck.position.x, edgeCheck.position.y - entityData.edgeCheckDistance));
        Gizmos.DrawLine(detectedCheck.position, new Vector2(detectedCheck.position.x + entityData.minDetectedDistance, detectedCheck.position.y));
    }

    // 设置刚体速度
    public virtual void SetVelocity(float moveSpeed)
    {
        movement.Set(moveSpeed * facingDirection, rb.velocity.y);
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
}