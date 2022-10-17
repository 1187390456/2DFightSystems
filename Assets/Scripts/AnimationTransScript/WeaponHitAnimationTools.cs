using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitAnimationTools : MonoBehaviour
{
    private AggressiveWeapon weapon;

    private void Awake()
    {
        weapon = transform.GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.AddList(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        weapon.RemoveList(collision);
    }
}