using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Target : MonoBehaviour
{
    [SerializeField] private float health;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health < 1) Destroy(gameObject);
    }

    public void InstantKill() => TakeDamage(health);


}
