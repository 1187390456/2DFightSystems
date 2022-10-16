using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core;

    public virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();
    }

    public virtual void Update()
    {
    }

    public virtual void OnDrawGizmos()
    {
    }
}