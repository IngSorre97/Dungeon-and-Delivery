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
        music.Stop();
        music.clip = peacefulMusic;
        music.loop = true;
        music.Play();
    }

    public void StartBattle(){
        music.Stop();
        music.clip = battleMusic;
        music.loop = true;
        music.Play();
    }

    public void Victory(){
        soundEffects.volume = 0.4f;
        music.Stop();
        soundEffects.clip = victory;
        soundEffects.Play();
    }

    public void Defeat(){
        music.Stop();
        soundEffects.volume = 0.05f;
        soundEffects.clip = defeat;
        soundEffects.Play();
        
    }

    public void MeleeAttack(){
        soundEffects.volume = 0.4f;
        soundEffects.clip = melee;
        soundEffects.Play();
    }

    public void MagicAttack(){
        soundEffects.volume = 0.4f;
        soundEffects.clip = magic;
        soundEffects.Play();
    }
}
