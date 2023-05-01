using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private TextMeshProUGUI playerDamage;

    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject buttons;

    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);

        GameManager.onStatsChanged += OnStatsChanged;
    }

    private void OnStatsChanged(Player player){
        playerHealth.text = $"{player.currentHealth.ToString()} / {player.maxHealth.ToString()}";
        playerDamage.text = $"{player.minDamage.ToString()} - {player.maxDamage.ToString()}";
    }

    public void GameOver(){
        gameOverScreen.SetActive(true);
        buttons.SetActive(false);
    }

    public void Victory(){
        victoryScreen.SetActive(true);
        buttons.SetActive(false);
    }

    public void OnRetryClicked(){
        gameOverScreen.SetActive(false);
        victoryScreen.SetActive(false);
        buttons.SetActive(true);
        GameManager.Instance.OnRetryClicked();
    }

    public void OnNextClicked(){
        victoryScreen.SetActive(false);
        buttons.SetActive(true);
        GameManager.Instance.OnNextClicked();
    }




}
