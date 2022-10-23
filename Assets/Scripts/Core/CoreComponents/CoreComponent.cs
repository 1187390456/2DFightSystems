using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core;
    protected Transform target;
    protected Movement movement => core.movement;
    protected PlayerState playerState => core.playerState;
    protected EnemyCollisionSenses enemySense => core.enemyCollisionSenses;
    protected PlayerCollisionSenses playerSense => core.playerCollisionSenses;

    protected PlayerStats playerStats => core.playerStats;
    protected EnemyStats enemyStats => core.enemyStats;

    public virtual void Awake()
    {
        core = transform.GetComponentInParent<Core>();
        target = transform.parent.parent;
    }

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnDrawGizmos()
    {
    }

    public virtual void FixedUpdate()
    {
    }
}