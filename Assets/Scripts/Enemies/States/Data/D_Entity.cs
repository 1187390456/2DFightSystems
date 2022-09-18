using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30f;//最大生命值

    public float damageHopSpeed = 3f;//伤害跳跃速度

    public float wallCheckDistance = 0.2f; //检查墙的距离
    public float ledgeCheckDistance = 0.4f;//检查臂架的距离
    public float groundCheckRadius = 0.3f; //地面检查半径

    public float minAgroDistance = 3f;//最小仇恨距离
    public float maxAgroDistance = 4f;//最大仇恨距离

    public float stunResistance = 3f;//眩晕阻力
    public float stunRecoveryTime = 2f;//眩晕恢复时间

    public float closeRangeActionDistance = 1f;//近距离动作距离

    public Transform hitParticle;//撞击粒子

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
