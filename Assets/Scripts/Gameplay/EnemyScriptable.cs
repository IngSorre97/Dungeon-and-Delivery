using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyScriptable : ScriptableObject
{
    public string enemyName;
    public int minDamage;
    public int maxDamage;
    public int health;
    public DamageTypes damageType;
    public GameObject enemyPrefab;
}