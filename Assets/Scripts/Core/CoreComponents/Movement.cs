using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : CoreComponent
{
    public int facingDireciton { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public void SetVelocityX(float velocity) => rb.velocity = new Vector2(velocity, rb.velocity.y);

    public void SetVelocitY(float velocity) => rb.velocity = new Vector2(rb.velocity.x, velocity);

    public void SetVelocity(float velocity, Vector2 direction) => rb.velocity = direction * velocity;

    public void SetVelocityZero() => rb.velocity = Vector2.zero;

    public void SetPlayerMove(float velocity) => SetVelocityX(velocity * InputManager.Instance.xInput);

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        rb.velocity = new Vector2(velocity * angle.x * direction, velocity * angle.y);
    }

    public void SetTurn()
    {
        facingDireciton *= -1;
        rb.transform.Rotate(0.0f, 180.0f, 0.0f);
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
        rb = GetComponentInParent<Rigidbody2D>();
    }
}