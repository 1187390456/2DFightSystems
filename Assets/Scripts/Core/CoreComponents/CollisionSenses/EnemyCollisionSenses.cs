using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollisionSenses : CollisionSenses
{
    [Header("墙壁检测点")] public Transform wallCheck;
    [Header("边缘检测点")] public Transform edgeCheck;
    [Header("警备检测点")] public Transform detectedCheck;
    [Header("地面检测点")] public Transform groundCheck;
    [Header("近战攻击检测点")] public Transform meleeAttackCheck;
    [Header("远程攻击检测点")] public Transform remoteAttackCheck;

    [Header("墙壁检测距离")] public float wallCheckDistance = 0.1f;
    [Header("地面检测距离")] public float groundCheckDistance = 0.1f;
    [Header("边缘检测距离")] public float edgeCheckDistance = 1.0f;
    [Header("最大警备检测距离")] public float maxDetectedDistance = 13.0f;
    [Header("最小警备检测距离")] public float minDetectedDistance = 10.0f;
    [Header("近战攻击检测距离")] public float meleeDistance = 2.7f;
    [Header("近战攻击半径")] public float meleeAttackRadius = 2.0f;

    // 墙壁检测
    public virtual bool Wall() => Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, LayerMask.GetMask("Ground"));

    // 地面检测
    public virtual bool Ground() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, LayerMask.GetMask("Ground"));

    // 边缘检测
    public virtual bool Edge() => Physics2D.Raycast(edgeCheck.position, Vector2.down, edgeCheckDistance, LayerMask.GetMask("Ground"));

    // 最大警备点检测
    public virtual bool MaxDetected() => Physics2D.Raycast(detectedCheck.position, transform.right, maxDetectedDistance, LayerMask.GetMask("Player"));

    // 最小警备点检测
    public virtual bool MinDetected() => Physics2D.Raycast(detectedCheck.position, transform.right, minDetectedDistance, LayerMask.GetMask("Player"));

    // 近战攻击检测
    public virtual bool MeleeAttack() => Physics2D.Raycast(detectedCheck.position, transform.right, meleeDistance, LayerMask.GetMask("Player"));

    // 保护条件
    public virtual bool Protect() => !Edge() || Wall();

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (core != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
            Gizmos.DrawLine(edgeCheck.position, new Vector2(edgeCheck.position.x, edgeCheck.position.y - edgeCheckDistance));
            Gizmos.DrawLine(detectedCheck.position, new Vector2(detectedCheck.position.x + meleeDistance, detectedCheck.position.y));
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
            Gizmos.DrawWireSphere(new Vector2(detectedCheck.position.x + minDetectedDistance * movement.facingDireciton, detectedCheck.position.y), 0.5f);
            Gizmos.DrawWireSphere(new Vector2(detectedCheck.position.x + maxDetectedDistance * movement.facingDireciton, detectedCheck.position.y), 0.5f);
            Gizmos.DrawWireSphere(meleeAttackCheck.transform.position, meleeAttackRadius);
        }
    }
}