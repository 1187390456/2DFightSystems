using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base", menuName = "Data/Player Data/State Data/Base Data")]
public class D_P_Base : ScriptableObject
{
    [Header("地面检测盒大小")] public Vector2 groundCheckSize = new Vector2(0.0f, 0.0f);

    [Header("移动速度")] public float moveSpeed = 10.0f;
    [Header("跳跃力度")] public float jumpForce = 16.0f;
    [Header("跳跃次数")] public int jumpCount = 3;
    [Header("土狼时间")] public float graceTime = 0.5f;
}