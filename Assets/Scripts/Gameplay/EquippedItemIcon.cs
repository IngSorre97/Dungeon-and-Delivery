using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemIcon : MonoBehaviour
{
    public void SetIcon(Sprite icon)
    {
        GetComponent<Image>().sprite = icon;
    }
}
