using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToScript : MonoBehaviour
{
    public E_Attack attackState; //  攻击状态

    // 近战攻击开始回调引用

    public void StartAttack()
    {
        attackState.StartAttack();
    }

    // 近战攻击结束回调引用
    public void FinishAttack()
    {
        attackState.FinishAttack();
    }
}