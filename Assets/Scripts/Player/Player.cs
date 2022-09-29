using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator at { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}