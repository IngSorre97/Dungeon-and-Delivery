using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootChoice : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI tooltip;
    private Item underlyingItem;
    public void InitWithItem(Item item)
    {
        underlyingItem = item;
        icon.sprite = item.icon;
        itemName.text = item.itemName;
        tooltip.text = item.tooltip;
    }

    public void OnPick()
    {
        if (underlyingItem)
        {
            
            GameManager.Instance.PlayNextMove();
            if (!underlyingItem.consumable)
            {
                UIManager.Instance.AddEquipItem(underlyingItem.icon);
                GameManager.Instance.EquipItem(underlyingItem);
            }
            else
            {
                GameManager.Instance.ConsumeItem(underlyingItem);
            }
                
            UIManager.Instance.CloseLootChoice();
        }
    }
}
