using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Vigilant : E_State
{
    public bool isVigilantTimeOver;//警惕时间是否结束
    public float vigilantTime = 1f;//警惕时间
    protected D_E_Vigilant vigilantData;
    public E_Vigilant(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Vigilant vigilantData) : base(stateMachine, entity, animName)
    {
        this.vigilantData = vigilantData;
    }

    public override void Enter()
    {
        base.Enter();
        vigilantTime = vigilantData.vigilantTime;
        isVigilantTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        vigilantTime -= Time.deltaTime;
        if (vigilantTime <= 0)
        {
            isVigilantTimeOver = true;
        }
    }
}
