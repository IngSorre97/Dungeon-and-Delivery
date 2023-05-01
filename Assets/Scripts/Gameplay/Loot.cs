using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{

    private const int LOOT_SIZE = 3;
    [SerializeField] private SpriteRenderer[] renderers;
    public List<Item> itemList = new List<Item>();
    public bool hasItems => itemList.Count > 0;
    [SerializeField] private List<Item> availableItems;


    public void InitializeLoot()
    {
        for (int i = 0; i < LOOT_SIZE; i++)
        {
            itemList.Add(randomItem());
            renderers[i].sprite = itemList[i].icon;
        }
    }

    private Item randomItem()
    {
        //TODO scelta random pesata su rarity
        int[] weights = availableItems.Select(item => item.rarity).ToArray();
        System.Random rnd = new System.Random();
        return RandomUtils.Choice(rnd, availableItems, weights);
    }

    public void Clear()
    {
        itemList = new List<Item>();
        foreach (SpriteRenderer sr in renderers)
        {
            sr.sprite = null;
        }


    }

}
