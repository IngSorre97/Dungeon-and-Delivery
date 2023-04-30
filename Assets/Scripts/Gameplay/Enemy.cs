using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DamageTypes damageType;
    public int maxHealth;
    public int currentHealth;
    
    public int minAttack;
    public int maxAttack;

    public bool TakeDamage(int damage){
        currentHealth -= damage;
        if (currentHealth <= 0) return true;
        else return false;
    }
}
