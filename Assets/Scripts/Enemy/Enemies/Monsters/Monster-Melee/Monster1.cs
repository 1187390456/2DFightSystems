using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : E_Entity
{
    public Monster1_Dead dead; // ����
    public Monster1_Idle idle; // �ƶ�
    public Monster1_Move move; // ����
    public Monster1_Detected detected; // ����
    public Monster1_Charge charge; // ���
    public Monster1_MeleeAttack meleeAttack; // ��ս����
    public Monster1_FindPlayer findPlayer; // Ѱ�����
    public Monster1_Ability1 ability1; // ����1

    [Header("��������")] public D_E_Dead deadData;
    [Header("��������")] public D_E_Idle idleData;
    [Header("�ƶ�����")] public D_E_Move moveData;
    [Header("��������")] public D_E_Detected detectedData;
    [Header("�������")] public D_E_Charge chargeData;
    [Header("��ս��������")] public D_E_MeleeAttack meleeAttackData;
    [Header("Ѱ���������")] public D_E_FindPlayer findPlayerData;
    [Header("����1����")] public D_E_Ability1 ability1Data;

    private void Start()
    {
        move = new Monster1_Move(stateMachine, this, "move", moveData, this);
        idle = new Monster1_Idle(stateMachine, this, "idle", idleData, this);
        dead = new Monster1_Dead(stateMachine, this, "dead", deadData, this);
        detected = new Monster1_Detected(stateMachine, this, "detected", detectedData, this);
        charge = new Monster1_Charge(stateMachine, this, "charge", chargeData, this);
        findPlayer = new Monster1_FindPlayer(stateMachine, this, "findPlayer", findPlayerData, this);
        meleeAttack = new Monster1_MeleeAttack(stateMachine, this, "meleeAttack", meleeAttackCheck, meleeAttackData, this);
        ability1 = new Monster1_Ability1(stateMachine, this, "ability1", ability1Data, this);
        stateMachine.Init(move);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackCheck.transform.position, meleeAttackData.meleeAttackRadius);
    }

    // �������
    private bool CheckDead()
    {
        return isDead && stateMachine.currentState != dead;
    }

    // �������
    private bool CheckHurt()
    {
        return isHurting && stateMachine.currentState != detected;
    }

    public override void Update()
    {
        base.Update();
        if (CheckDead())
        {
            stateMachine.ChangeState(dead);
        }
        else if (CheckHurt())
        {
            stateMachine.ChangeState(detected);
        }
    }
}