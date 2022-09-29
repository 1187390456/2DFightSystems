using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    [SerializeField] [Header("最大生命值")] private float maxHealth = 999.0f;
    public static PlayerStates Instance { get; private set; } // 单例
    private float currentHealth;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

    // 减少生命
    public void DecreaseHealth(float dagameValue)
    {
        currentHealth -= dagameValue;
        if (currentHealth > 0.0f)
        {
        }
        else if (currentHealth <= 0.0f)
        {
            Died();
        }
    }

    // 死亡
    private void Died()
    {
        EffectBox.Instance.Chunk(transform.position);
        EffectBox.Instance.Blood(transform.position);
        Destroy(gameObject);
        GameManager.Instance.Rebirth();
    }
}