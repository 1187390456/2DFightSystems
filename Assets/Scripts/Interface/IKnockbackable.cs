using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable
{
    void Knckback(float velocity, Vector2 angle, int direction);
}