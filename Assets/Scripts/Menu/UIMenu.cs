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

    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioSource music;

    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip typing;
    private int index = 0;
    [SerializeField] private GameObject nextButton;

    private void Start(){
        music.clip = backgroundMusic;
        music.loop = true;
        music.Play();

        soundEffects.clip = typing;
        soundEffects.loop = false;
        soundEffects.Play();

        player.isTalking = true;
        player.StartTalking();

        user.isTalking = false;
        user.StartTalking();

        dialogueBox.text = dialogues[0];
    }

    public void OnNextClicked(){
        soundEffects.Stop();
        soundEffects.Play();
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
