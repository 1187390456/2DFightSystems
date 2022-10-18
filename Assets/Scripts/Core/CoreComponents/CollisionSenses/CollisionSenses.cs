using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public BoxCollider2D collider2d { get; private set; }
    public Vector2 normalColliderSize { get; private set; }
    public Vector2 normalColliderOffset { get; private set; }

    private Vector2 workSpace;

    public bool Android() => Application.platform == RuntimePlatform.Android;

    public void SetHalfCollider()
    {
        var offset = collider2d.offset;
        var size = collider2d.size;
        workSpace.Set(size.x, size.y / 2);
        offset.y += (size.y / 2 - size.y) / 2;
        collider2d.size = workSpace;
        collider2d.offset = offset;
    }

    public void SetResumeCollider()
    {
        collider2d.size = normalColliderSize;
        collider2d.offset = normalColliderOffset;
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void Awake()
    {
        base.Awake();
        collider2d = target.GetComponent<BoxCollider2D>();
        normalColliderSize = collider2d.size;
        normalColliderOffset = collider2d.offset;
    }
}