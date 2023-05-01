using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private AnimatedUI player;
    [SerializeField] private AnimatedUI user;

    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] List<string> dialogues;
    private int index = 0;
    [SerializeField] private GameObject nextButton;

    private void Start(){
        player.isTalking = true;
        player.StartTalking();

        user.isTalking = false;
        user.StartTalking();

        dialogueBox.text = dialogues[0];
    }

    public void OnNextClicked(){
        player.isTalking = !player.isTalking;
        user.isTalking = !user.isTalking;
        index++;
        dialogueBox.text = dialogues[index];
        
        if (index == dialogues.Count - 1)
            nextButton.SetActive(false);
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }


}
