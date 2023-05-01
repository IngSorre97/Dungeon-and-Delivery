using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Arc : MonoBehaviour
{
    [SerializeField] private Node firstNode;
    [SerializeField] private Node secondNode;
    [SerializeField] private bool isVertical;
    [SerializeField] private Transform spotEnemy;
    public Enemy enemy;
    public bool hasEnemy => ( enemy != null );
    public Enemy getEnemy => enemy;
    [SerializeField] private Transform spotPlayerLeft;
    [SerializeField] private Transform spotPlayerRight;

    [Header("Enemy UI")]
    [SerializeField] private Transform enemyUI;
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private Image alertHealth;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private Image damageType;
    [SerializeField] private Image alertDamage;

    private bool lastRight;

    public void Initialize(){
        if (isVertical){
            enemyUI.Rotate(0,0,-90);
            spotEnemy.Rotate(0,0,-90);
            spotPlayerLeft.Rotate(0,0,-90);
            spotPlayerRight.Rotate(0,0,-90);
        }

        if (enemy == null) Destroy(enemyUI.gameObject);
        else {
            health.text = $"{enemy.maxHealth}";
            damage.text = $"{enemy.minDamage} | {enemy.maxDamage}";
            switch (enemy.damageType){
                case DamageTypes.Normal:
                    damageType.sprite = SpriteData.Instance.normalAttack;
                    break;
                case DamageTypes.Special:
                    damageType.sprite = SpriteData.Instance.specialAttack;
                    break;
            }
        }
    }

    public Node GetOtherNode(Node node){
        if (firstNode != node && secondNode != node) return null;
        Node returnNode = firstNode == node ? secondNode : firstNode;
        lastRight = returnNode.isRight(returnNode);
        return returnNode;
    }

    public Transform GetClosestSpot(Node node){
        return ( Vector3.Distance(node.gameObject.transform.position, spotPlayerLeft.position) < Vector3.Distance(node.gameObject.transform.position, spotPlayerRight.position) ) ?
            spotPlayerRight : spotPlayerLeft;
    }

    public bool isRight(){
        return lastRight;
    }
}
