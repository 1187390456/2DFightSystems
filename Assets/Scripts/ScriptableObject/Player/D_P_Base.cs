using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base", menuName = "Data/Player Data/State Data/Base Data")]
public class D_P_Base : ScriptableObject
{
    [Header("地面检测盒大小")] public Vector2 groundCheckSize = new Vector2(0.55f, 0.02f);
    [Header("墙壁检测长度")] public float wallCheckDistance = 0.4f;

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
}