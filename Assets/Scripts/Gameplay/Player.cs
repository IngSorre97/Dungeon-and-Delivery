using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Node currentNode {get; private set;}
    public DamageTypes currentDamage {get; private set;}

    [SerializeField] private GameObject spriteObject;

    public int maxHealth;
    public int currentHealth;
    public int minDamage;
    public int maxDamage;

    private bool isRight = true;


    public void Initialize(Node startingNode){
        currentNode = startingNode;
    }

    public void SetNewDestination(Node destinationNode, bool isRight){
        if (GameManager.Instance.debug) Debug.Log($"Last movement was towards {(isRight ? "right" : "left")}");
        currentNode = destinationNode;
        if (this.isRight != isRight){
            spriteObject.transform.Rotate(0,180,0);
            this.isRight = isRight;
        }
    }

    public void EquipItem(Item item)
    {
        Debug.Log($"Equip : {item.itemName}");
        applyItemEffects(item);
    }

    public void ConsumeItem(Item item)
    {
        Debug.Log($"Consume : {item.itemName}");
        applyItemEffects(item);
    }

    private void applyItemEffects(Item item)
    {
        maxHealth += item.maxHealthModifier;
        minDamage += item.minAttackModifier;
        maxDamage += item.maxAttackModifier;
        if (item.canOverheal)
            currentHealth += item.currHealthModifier;
        else
            currentHealth = Mathf.Min(currentHealth + item.currHealthModifier, maxHealth);
        GameManager.onStatsChanged?.Invoke(this);
    }
}
