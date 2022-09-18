using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField]
    float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;
    [SerializeField]
    bool applyKnockback;
    [SerializeField]
    GameObject hitParticle;

    float currentHealth, knockbackStart;

    int playerFacingDirection;//玩家面临方向

    bool playerOnLeft, knockback;

    Player pc;
    GameObject aliveGo, brolkenTopGo, brokenBotGo;
    Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    Animator aliveAnim;

    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<Player>();

        aliveGo = transform.Find("Alive").gameObject;
        brolkenTopGo = transform.Find("Broken Top").gameObject;
        brokenBotGo = transform.Find("Broken Bottom").gameObject;

        aliveAnim = aliveGo.GetComponent<Animator>();
        rbAlive = aliveGo.GetComponent<Rigidbody2D>();
        rbBrokenTop = brolkenTopGo.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGo.GetComponent<Rigidbody2D>();

        aliveGo.SetActive(true);
        brolkenTopGo.SetActive(false);
        brokenBotGo.SetActive(false);
    }
    private void Update()
    {
        CheckKnockback();
    }
    //损坏
    void Damage(AttackDetails details)
    {
        currentHealth -= details.damageAmount;

        if (details.position.x < aliveGo.transform.position.x)
        {
            playerFacingDirection = 1;
        }
        else
        {
            playerFacingDirection = -1;
        }

        Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if (applyKnockback && currentHealth > 0.0f)
        {
            //击退
            Knockback();
        }

        if (currentHealth <= 0.0f)
        {
            //死亡
            Die();
        }
    }
    //击退
    void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }
    //检查击退
    void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }
    //死亡
    void Die()
    {
        aliveGo.SetActive(false);
        brolkenTopGo.SetActive(true);
        brokenBotGo.SetActive(true);

        brolkenTopGo.transform.position = aliveGo.transform.position;
        brokenBotGo.transform.position = aliveGo.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
