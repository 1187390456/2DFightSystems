using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
    public AttackState attackState;//攻击状态
    //触发攻击
    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }
    //完成攻击
    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
