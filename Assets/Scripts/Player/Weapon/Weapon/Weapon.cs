using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("武器数据")] public SO_WeaponData weaponData;
    protected Animator baseAt;
    protected Animator weaponAt;
    protected P_Attack state;
    protected Movement movement;
    protected int attackIndex = 0;

    public virtual void InitState(P_Attack state, Movement movement)
    {
        this.state = state;
        this.movement = movement;
    }

    protected virtual void Awake()
    {
        baseAt = transform.Find("Base").GetComponent<Animator>();
        weaponAt = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void Update()
    {
    }

    public virtual void Enter()
    {
        gameObject.SetActive(true);
        baseAt.SetBool("attack", true);
        weaponAt.SetBool("attack", true);
    }

    public virtual void Exit()
    {
        baseAt.SetBool("attack", false);
        weaponAt.SetBool("attack", false);
        gameObject.SetActive(false);
    }

    public virtual void AnimationStart() => state.StartAnimation();

    public virtual void AnimationDone() => state.FinishAnimation();

    public virtual void MoveStart() => state.MoveStart(weaponData.moveSpeed[attackIndex]);

    public virtual void MoveStop() => state.MoveStop(0.0f);

    public virtual void TurnOff() => state.SetTurnOff();

    public virtual void TurnOn() => state.SetTurnOn();
}