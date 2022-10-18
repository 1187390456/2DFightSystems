using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement movement { get; private set; }

    public PlayerCollisionSenses playerCollisionSenses { get; private set; }

    public EnemyCollisionSenses enemyCollisionSenses { get; private set; }

    public InputAction inputAction { get; private set; }

    public PlayerState playerState { get; private set; }

    public EnemyState enemyState { get; private set; }
    public Combat combat { get; private set; }

    private void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        playerCollisionSenses = GetComponentInChildren<PlayerCollisionSenses>();
        enemyCollisionSenses = GetComponentInChildren<EnemyCollisionSenses>();
        inputAction = GetComponentInChildren<InputAction>();
        playerState = GetComponentInChildren<PlayerState>();
        enemyState = GetComponentInChildren<EnemyState>();
        combat = GetComponentInChildren<Combat>();
    }
}