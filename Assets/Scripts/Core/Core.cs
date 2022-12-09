using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public InputAction inputAction { get => GetComponentInChildren<InputAction>(); }

    public Movement movement { get => GetComponentInChildren<Movement>(); }

    public EnemyCombat enemyCombat { get => GetComponentInChildren<EnemyCombat>(); }

    public PlayerCombat playerCombat { get => GetComponentInChildren<PlayerCombat>(); }

    public PlayerCollisionSenses playerCollisionSenses { get => GetComponentInChildren<PlayerCollisionSenses>(); }

    public EnemyCollisionSenses enemyCollisionSenses { get => GetComponentInChildren<EnemyCollisionSenses>(); }

    public PlayerState playerState { get => GetComponentInChildren<PlayerState>(); }

    public PlayerStats playerStats { get => GetComponentInChildren<PlayerStats>(); }

    public EnemyStats enemyStats { get => GetComponentInChildren<EnemyStats>(); }
}