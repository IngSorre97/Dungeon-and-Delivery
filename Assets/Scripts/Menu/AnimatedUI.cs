using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> idleSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> talkingSprites = new List<Sprite>();

    [SerializeField] private Image image;

    public bool isTalking;

    int index = 0;
    public void StartTalking(){
        StartCoroutine(Talking());
    }

    private IEnumerator Talking(){
        bool storedTalking = isTalking;
        while(true){
            if (storedTalking != isTalking)
                {
                    index = 0;
                    storedTalking = isTalking;
                }
            image.sprite = isTalking ? talkingSprites[index] : idleSprites[index];
            int maxIndex = isTalking ? talkingSprites.Count : idleSprites.Count;
            index++;
            if (index == maxIndex) index = 0;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
