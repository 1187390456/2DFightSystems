using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base", menuName = "Data/Player Data/State Data/Base Data")]
public class D_P_Base : ScriptableObject
{
    [Header("地面检测盒大小")] public Vector2 groundCheckSize = new Vector2(0.55f, 0.02f);
    [Header("墙壁检测长度")] public float wallCheckDistance = 0.4f;
    [Header("头部检测半径")] public float topCheckRadius = 0.54f;

    [Header("移动速度")] public float moveSpeed = 10.0f;

    [Header("跳跃力度")] public float jumpForce = 30.0f;
    [Header("跳跃次数")] public int jumpCount = 2;
    [Header("土狼时间")] public float graceTime = 0.5f;
    [Header("跳跃空气动力乘数")] public float jumpAirMultiplier = 0.5f;

    [Header("滑墙速度")] public float sildeSpeed = 8.0f;
    [Header("墙上爬行速度")] public float climbSpeed = 12.0f;

    [Header("跳墙力度")] public float wallJumpForce = 30.0f;
    [Header("跳墙角度")] public Vector2 wallJumpAngle = new Vector2(1.0f, 1.6f);
    [Header("跳墙时间")] public float wallJumpTime = 0.4f;
    [Header("跳墙土狼时间")] public float wallJumpGraceTime = 0.6f;

    [Header("冲刺冷却时间")] public float dashCoolDown = 1.0f;
    [Header("冲刺动力感质量")] public float dashDrag = 10.0f;
    [Header("冲刺速度")] public float dashSpeed = 30.0f;
    [Header("冲刺时间")] public float dashTime = 0.2f;
    [Header("冲刺动力乘数")] public float dashMultiplier = 0.2f;
    [Header("最大按住时间")] public float maxHoldTime = 2.0f;
    [Header("冲刺时间刻度")] public float dashTimeScale = 0.25f;
    [Header("冲刺残影生成间距")] public float afterImageSpace = 0.5f;

    [Header("蹲伏移动速度")] public float crouchMoveSpeed = 4.0f;

    [Header("击退时间")] public float knockbackTime = 0.2f;
    [Header("击退速度")] public float knockbackSpeed = 20.0f;
    [Header("击退角度")] public Vector2 knockbackAngle = new Vector2(1.0f, 1.0f);

    [Header("最大生命值")] public float maxHealth = 10;
}