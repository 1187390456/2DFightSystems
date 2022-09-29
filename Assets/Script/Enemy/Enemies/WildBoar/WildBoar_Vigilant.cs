using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Vigilant : E_Vigilant
{
    private WildBoar wildBoar;
    public WildBoar_Vigilant(E_StateMachine stateMachine, E_Entity entity, string animName, D_E_Vigilant vigilantData, WildBoar wildBoar) : base(stateMachine, entity, animName, vigilantData)
    {
        this.wildBoar = wildBoar;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isVigilantTimeOver)
        {
            entity.stateMachine.ChangeState(wildBoar.dash);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
