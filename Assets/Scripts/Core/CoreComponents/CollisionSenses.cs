using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    [Header("地面检测")] public Transform groundCheck;
    [Header("墙壁检测")] public Transform wallCheck;
    [Header("边缘检测")] public Transform ledgeCheck;
    [Header("头部检测")] public Transform topCheck;
    [Header("地面检测盒子大小")] public Vector2 groundCheckSize = new Vector2(0.55f, 0.04f);
    [Header("墙壁检测距离")] public float wallCheckDistance = 0.4f;
    [Header("头部检测半径")] public float topCheckRadius = 0.54f;
    public BoxCollider2D collider2d { get; private set; }
    public Vector2 normalColliderSize { get; private set; }
    public Vector2 normalColliderOffset { get; private set; }

    private Vector2 workSpace;

    public bool Android() => Application.platform == RuntimePlatform.Android;

    public bool Ground() => Physics2D.BoxCast(groundCheck.position, groundCheckSize, 0.0f, transform.right, 0.0f, LayerMask.GetMask("Ground"));

    public bool Wall() => Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool BackWall() => Physics2D.Raycast(wallCheck.position, -transform.right, wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool Ledge() => Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool Top() => Physics2D.OverlapCircle(topCheck.position, topCheckRadius, LayerMask.GetMask("Ground"));

    public void SetHalfCollider()
    {
        var offset = collider2d.offset;
        var size = collider2d.size;
        workSpace.Set(size.x, size.y / 2);
        offset.y += (size.y / 2 - size.y) / 2;
        collider2d.size = workSpace;
        collider2d.offset = offset;
    }

    public void SetResumeCollider()
    {
        collider2d.size = normalColliderSize;
        collider2d.offset = normalColliderOffset;
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x - wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(ledgeCheck.position, new Vector2(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y));
        Gizmos.DrawWireSphere(topCheck.position, topCheckRadius);
    }

    public override void Awake()
    {
        base.Awake();
        collider2d = target.GetComponent<BoxCollider2D>();
        normalColliderSize = collider2d.size;
        normalColliderOffset = collider2d.offset;
    }
}