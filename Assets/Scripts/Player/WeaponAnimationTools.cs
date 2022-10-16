using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationTools : MonoBehaviour
{
    private Weapon weapon;

    private void Awake() => weapon = GetComponentInParent<Weapon>();

    public void AnimationDone() => weapon.AnimationDone();
}