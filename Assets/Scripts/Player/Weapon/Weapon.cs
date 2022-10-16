using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("武器数据")] public SO_WeaponData weaponData;
    protected Animator baseAt;
    protected Animator weaponAt;
    protected P_Attack state;
    protected int attackIndex = 0;

    protected virtual void Awake()
    {
        baseAt = transform.Find("Base").GetComponent<Animator>();
        weaponAt = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void Enter()
    {
        if (attackIndex >= weaponData.moveSpeed.Length) attackIndex = 0;
        gameObject.SetActive(true);
        baseAt.SetBool("attack", true);
        weaponAt.SetBool("attack", true);
        baseAt.SetInteger("attackIndex", attackIndex);
        weaponAt.SetInteger("attackIndex", attackIndex);
    }

    public virtual void Exit()
    {
        baseAt.SetBool("attack", false);
        weaponAt.SetBool("attack", false);
        attackIndex++;
        gameObject.SetActive(false);
    }

    public virtual void AnimationDone() => state.FinishAnimation();

    public virtual void MoveStart() => state.MoveStart(weaponData.moveSpeed[attackIndex]);

    public virtual void MoveStop() => state.MoveStop(0.0f);

    public virtual void TurnOff() => state.SetTurnOff();

    public virtual void TurnOn() => state.SetTurnOn();

    public virtual void InitState(P_Attack state) => this.state = state;
}