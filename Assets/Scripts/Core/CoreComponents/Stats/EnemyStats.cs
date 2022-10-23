using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    [Header("最大击晕次数")] public int maxStunCount = 3;
    [HideInInspector] public int currentStunCount;

    public override void Awake()
    {
        base.Awake();
        ResetStunCount();
    }

    public override void DecreaseHealth(float amount)
    {
        base.DecreaseHealth(amount);
        target.SendMessage("DecreaseHealthCallBack");
    }

    public virtual void DecreaseStunCount() => currentStunCount--;

    public virtual void ResetStunCount() => currentStunCount = maxStunCount;
}