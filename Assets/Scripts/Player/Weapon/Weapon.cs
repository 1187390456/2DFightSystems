using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator baseAt;
    protected Animator weaponAt;
    protected P_State state;

    protected virtual void Awake()
    {
        baseAt = transform.Find("Base").GetComponent<Animator>();
        weaponAt = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
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

    public virtual void AnimationDone() => state.FinishAnimation();

    public virtual void InitState(P_State state) => this.state = state;
}