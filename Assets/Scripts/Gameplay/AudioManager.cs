using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource soundEffects;
    [SerializeField] private AudioClip peacefulMusic;
    [SerializeField] private AudioClip battleMusic;

    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioClip defeat;
    [SerializeField] private AudioClip melee;
    [SerializeField] private AudioClip magic;


    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void StartGame(){
        music.clip = peacefulMusic;
        music.loop = true;
        music.Play();
    }

    public void StartBattle(){
        music.clip = battleMusic;
        music.loop = true;
        music.Play();
    }

    public void Victory(){
        soundEffects.clip = victory;
        soundEffects.Play();
    }

    public void Defeat(){
        soundEffects.clip = defeat;
        soundEffects.Play();
    }

    public void MeleeAttack(){
        soundEffects.clip = melee;
        soundEffects.Play();
    }

    public void MagicAttack(){
        soundEffects.clip = magic;
        soundEffects.Play();
    }
}
