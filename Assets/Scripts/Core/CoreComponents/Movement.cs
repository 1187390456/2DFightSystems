using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : CoreComponent
{
    public int facingDireciton { get; private set; }
    public Vector3 transRight { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public bool canSetVelocity { get; set; }
    public float rbX => rb.velocity.x;
    public float rbY => rb.velocity.y;

    public void SetVelocityX(float velocity)
    { if (canSetVelocity) rb.velocity = new Vector2(velocity * facingDireciton, rb.velocity.y); }

    public void SetVelocityY(float velocity)
    { if (canSetVelocity) rb.velocity = new Vector2(rb.velocity.x, velocity); }

    public void SetVelocity(float velocity, Vector2 direction)
    { if (canSetVelocity) rb.velocity = direction * velocity; }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        if (canSetVelocity)
        {
            angle.Normalize();
            rb.velocity = new Vector2(velocity * angle.x * direction, velocity * angle.y);
        }
    }

    public void SetVelocityZero()
    { if (canSetVelocity) rb.velocity = Vector2.zero; }

    public void SetPlayerMove(float velocity) => SetVelocityX(velocity * Mathf.Abs(InputManager.Instance.xInput));

    public void SetHoldStatic(Vector2 holdPos)
    {
        target.position = holdPos;
        SetVelocityZero();
    }

    public void SetTurn()
    {
        facingDireciton *= -1;
        target.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void CheckTurn()
    {
        if (facingDireciton == 1 && InputManager.Instance.xInput < 0)
        {
            SetTurn();
        }
        else if (facingDireciton == -1 && InputManager.Instance.xInput > 0)
        {
            SetTurn();
        }
    }

    public override void Awake()
    {
        base.Awake();
        facingDireciton = 1;
        canSetVelocity = true;
        rb = GetComponentInParent<Rigidbody2D>();
        transRight = target.right;
    }
}