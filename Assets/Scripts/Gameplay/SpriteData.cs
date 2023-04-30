using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteData : MonoBehaviour
{
    public static SpriteData Instance;

    public Sprite specialAttack;
    public Sprite normalAttack;
    public Sprite alert;
    public Sprite noAlert;

    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
