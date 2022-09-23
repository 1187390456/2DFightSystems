﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Detected", menuName = "Data/Enemy Data/State Data/Detected Data")]
public class D_E_Detected : ScriptableObject
{
    [Header("警备时间")] public float detectedTime = 0.8f;
}