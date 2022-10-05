using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    [Header("最大生命值")] public float maxHealth = 999.0f;
    public static PlayerStates Instance { get; private set; } // 单例
    [HideInInspector] public float currentHealth;
    [HideInInspector] public bool isDead = false;
    public GameObject canvas;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
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
        isDead = true;
        SetBtn(false);
        EffectBox.Instance.Chunk(transform.position);
        EffectBox.Instance.Blood(transform.position);
        Destroy(transform.parent.gameObject);
        GameManager.Instance.Rebirth();
    }

    // 隐藏所有按钮
    public void SetBtn(bool value)
    {
        canvas.transform.Find("button").gameObject.SetActive(value);
        canvas.transform.Find("Switch").gameObject.SetActive(value);
        canvas.transform.Find("move").gameObject.SetActive(value);
    }
}