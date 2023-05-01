using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public int maxAttackModifier;
    public int minAttackModifier;
    public int currHealthModifier;
    public int maxHealthModifier;
    public Sprite icon;
    public int rarity;
    public bool consumable;
    public string tooltip;
    public bool canOverheal;
}
