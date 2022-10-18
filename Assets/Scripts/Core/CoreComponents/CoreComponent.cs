using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core;
    protected Transform target;
    protected Movement movement => core.movement;
    protected EnemyCollisionSenses enemySense => core.enemyCollisionSenses;

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