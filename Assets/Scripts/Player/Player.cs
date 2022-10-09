using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [Header("地面检测点")] public Transform groundCheck;
    [Header("墙壁检测点")] public Transform wallCheck;

    [Header("玩家数据")] public D_P_Base playerData;

    public P_StateMachine stateMachine = new P_StateMachine();
    public Rigidbody2D rb { get; private set; }
    public Animator at { get; private set; }
    public InputManager inputManager { get; private set; }

    public int facingDireciton { get; private set; }

    #region 状态

    public P_Idle idle { get; private set; }
    public P_Move move { get; private set; }
    public P_Jump jump { get; private set; }
    public P_InAir inAir { get; private set; }
    public P_Land land { get; private set; }
    public P_Silde slide { get; private set; }

    #endregion 状态

    #region Unity回调

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();

        idle = new P_Idle(stateMachine, this, "idle", playerData);
        move = new P_Move(stateMachine, this, "move", playerData);
        jump = new P_Jump(stateMachine, this, "inAir", playerData);
        inAir = new P_InAir(stateMachine, this, "inAir", playerData);
        land = new P_Land(stateMachine, this, "land", playerData);
        slide = new P_Silde(stateMachine, this, "silde", playerData);
        stateMachine.Init(idle);

        facingDireciton = 1;
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, playerData.groundCheckSize);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerData.wallCheckDistance, wallCheck.position.y));
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    #endregion Unity回调

    #region InputManager

    public int GetXInput() => InputManager.Instance.xInput;

    public int GetYInput() => InputManager.Instance.yInput;

    public bool GetJumpInputStop() => InputManager.Instance.jumpInputStop;

    public void UseJumpInput() => InputManager.Instance.UseJumpInput();

    public void UseJumpInputStop() => InputManager.Instance.UseJumpInputStop();

    #endregion InputManager

    #region 回调函数

    private void StartAnimation() => stateMachine.currentState.StartAnimation();

    private void FinishAnimation() => stateMachine.currentState.FinishAnimation();

    #endregion 回调函数

    #region 设置

    public void SetVelocityX(float velocity) => rb.velocity = new Vector2(velocity, rb.velocity.y);

    public void SetVelocitY(float velocity) => rb.velocity = new Vector2(rb.velocity.x, velocity);

    public void SetTurn()
    {
        facingDireciton *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion 设置

    #region 检测状态

    public void CheckTurn()
    {
        if (facingDireciton == 1 && inputManager.xInput < 0)
        {
            SetTurn();
        }
        else if (facingDireciton == -1 && inputManager.xInput > 0)
        {
            SetTurn();
        }
    }

    public bool CheckGround() => Physics2D.BoxCast(groundCheck.position, playerData.groundCheckSize, 0.0f, transform.right, 0.0f, LayerMask.GetMask("Ground"));

    public bool ChechWall() => Physics2D.Raycast(wallCheck.position, transform.right, playerData.wallCheckDistance, LayerMask.GetMask("Ground"));

    public bool GroundCondition() => rb.velocity.y <= 0.01f && CheckGround();

    public bool JumpCondition() => InputManager.Instance.jumpInput && jump.ChechCanJump();

    #endregion 检测状态
}