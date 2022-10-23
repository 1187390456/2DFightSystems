using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [Header("最大生命值")] public float maxHealth = 100.0f;
    [HideInInspector] public float currentHealth;

    public override void Awake()
    {
        base.Awake();
        ResetHealth();
    }

    public virtual void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
    }

    public virtual void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public virtual void ResetHealth() => currentHealth = maxHealth;

    public virtual void ClearHealth() => currentHealth = 0;
}