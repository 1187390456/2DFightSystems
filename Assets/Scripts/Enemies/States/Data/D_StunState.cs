using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3f;//眩晕时间
    public float stunKnockbackTime = 0.2f;//击退时间
    public float stunKnockbackSpeed = 20f;//击退时间
    public Vector2 stunKnockbackAngle;//击退角度
}
