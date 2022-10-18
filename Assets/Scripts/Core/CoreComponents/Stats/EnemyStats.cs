using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    [Header("最大击晕次数")] public int maxStunCount = 5;
    [HideInInspector] public int currentStunCount;
    private E_StateMachine stateMachine => enemyState.stateMachine;

    public override void Awake()
    {
        base.Awake();
        currentStunCount = maxStunCount;
    }

    public override void DecreaseHealth(float amount)
    {
        base.DecreaseHealth(amount);
        if (currentHealth > 0)
        {
            if (StunConditon())
            {
                currentStunCount -= 1;
                if (currentStunCount <= 0)
                {
                    currentStunCount = maxStunCount;
                    enemyState.stateMachine.ChangeState(enemyState.stun);
                }
            }

            if (HurtCondition())
            {
                stateMachine.ChangeState(enemyState.hurt);
            }
        }
        else
        {
            currentHealth = 0;
            stateMachine.ChangeState(enemyState.dead);
        }
    }

    public override void IncreaseHealth(float amount)
    {
        base.IncreaseHealth(amount);
    }

    private bool HurtCondition() => stateMachine.currentState != enemyState.stun && stateMachine.currentState != enemyState.hurt;

    private bool StunConditon() => currentStunCount > 0 && stateMachine.currentState != enemyState.stun;
}