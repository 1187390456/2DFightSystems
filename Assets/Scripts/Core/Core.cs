using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement movement { get; private set; }
    public CollisionSenses collisionSenses { get; private set; }
    public InputAction inputAction { get; private set; }
    public PlayerState playerState { get; private set; }

    private void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collisionSenses = GetComponentInChildren<CollisionSenses>();
        inputAction = GetComponentInChildren<InputAction>();
        playerState = GetComponentInChildren<PlayerState>();
    }
}