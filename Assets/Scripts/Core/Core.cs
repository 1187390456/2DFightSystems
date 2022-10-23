using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public InputAction inputAction { get; private set; }
    public Movement movement { get; private set; }
    public EnemyCombat enemyCombat { get; private set; }
    public PlayerCombat playerCombat { get; private set; }

    public PlayerCollisionSenses playerCollisionSenses { get; private set; }

    public EnemyCollisionSenses enemyCollisionSenses { get; private set; }

    public PlayerState playerState { get; private set; }

    public PlayerStats playerStats { get; private set; }
    public EnemyStats enemyStats { get; private set; }

    private void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        inputAction = GetComponentInChildren<InputAction>();

        playerCollisionSenses = GetComponentInChildren<PlayerCollisionSenses>();
        playerState = GetComponentInChildren<PlayerState>();
        playerCombat = GetComponentInChildren<PlayerCombat>();
        playerStats = GetComponentInChildren<PlayerStats>();

        enemyCollisionSenses = GetComponentInChildren<EnemyCollisionSenses>();
        enemyStats = GetComponentInChildren<EnemyStats>();
        enemyCombat = GetComponentInChildren<EnemyCombat>();
    }
}