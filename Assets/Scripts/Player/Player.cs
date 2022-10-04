using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public P_StateMachine stateMachine = new P_StateMachine();
    public Rigidbody2D rb { get; private set; }
    public Animator at { get; private set; }
    public InputManager inputManager { get; private set; }

    public int facingDireciton { get; private set; }

    #region ×´Ì¬ Óë Êý¾Ý

    public P_E_Idle idle { get; private set; }
    public P_E_Move move { get; private set; }

    public D_P_Base playerData;

    #endregion ×´Ì¬ Óë Êý¾Ý

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        at = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();

        idle = new P_E_Idle(stateMachine, this, "idle", playerData);
        move = new P_E_Move(stateMachine, this, "move", playerData);
        stateMachine.Init(idle);

        facingDireciton = 1;
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    #region ÉèÖÃ

    public void SetVelocity(float velocity)
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }

    public void Turn()
    {
        facingDireciton *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion ÉèÖÃ

    #region ¼ì²â×´Ì¬

    public void CheckTurn()
    {
        if (facingDireciton == 1 && inputManager.inputX < 0)
        {
            Turn();
        }
        else if (facingDireciton == -1 && inputManager.inputX > 0)
        {
            Turn();
        }
    }

    #endregion ¼ì²â×´Ì¬
}