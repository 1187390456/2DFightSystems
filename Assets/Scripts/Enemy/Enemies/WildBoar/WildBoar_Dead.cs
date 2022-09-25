using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar_Dead : E_Dead
{
    private WildBoar wildBoar;

    public WildBoar_Dead(E_StateMachine stateMachine, E_Entity entity, string anmName, D_E_Dead deadData, WildBoar wildBoar) : base(stateMachine, entity, anmName, deadData)
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

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (isdeadOver)
        {
            stateMachine.ChangeState(wildBoar.move);
        }
    }
}