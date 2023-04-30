using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void OnHealthChanged(int current, int max);
    public static OnHealthChanged onHealthChanged;
    public delegate void OnDamageChanged(DamageTypes damageType, int min, int max);
    public static OnDamageChanged onDamageChanged;
    public Node currentNode {get; private set;}
    public DamageTypes currentDamage {get; private set;}

    public int maxHealth;
    public int currentHealth;
    public int minDamage;
    public int maxDamage;


    public void Initialize(Node startingNode){
        currentNode = startingNode;
    }

    public void SetNewDestination(Node destinationNode){
        currentNode = destinationNode;
    }
}
