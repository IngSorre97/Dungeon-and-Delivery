using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private TextMeshProUGUI playerDamage;

    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);

        Player.onHealthChanged += OnHealthChanged;
        Player.onDamageChanged += OnDamageChanged;
    }

    private void OnHealthChanged(int current, int max){
        playerHealth.text = $"{current} / {max}";
    }

    private void OnDamageChanged(DamageTypes type, int min, int max){
        playerDamage.text = $"{min} / {max}";
    }

    public void StartBattle(Arc arc){
        
    }
}
