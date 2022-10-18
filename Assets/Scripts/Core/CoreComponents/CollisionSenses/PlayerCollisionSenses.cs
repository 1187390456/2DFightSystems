using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionSenses : CollisionSenses
{
    [Header("墙壁检测")] public Transform wallCheck;
    [Header("地面检测")] public Transform groundCheck;
    [Header("边缘检测")] public Transform ledgeCheck;
    [Header("头部检测")] public Transform topCheck;

    [Header("头部检测半径")] public float topCheckRadius = 0.54f;
    [Header("墙壁检测距离")] public float wallCheckDistance = 0.4f;
    [Header("地面检测盒子大小")] public Vector2 groundCheckSize = new Vector2(0.55f, 0.04f);

    public bool Wall() => Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool Ground() => Physics2D.BoxCast(groundCheck.position, groundCheckSize, 0.0f, transform.right, 0.0f, LayerMask.GetMask("Ground"));

    public bool BackWall() => Physics2D.Raycast(wallCheck.position, -transform.right, wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool Ledge() => Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool Top() => Physics2D.OverlapCircle(topCheck.position, topCheckRadius, LayerMask.GetMask("Ground"));

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x - wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(ledgeCheck.position, new Vector2(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y));
        Gizmos.DrawWireSphere(topCheck.position, topCheckRadius);
    }
}