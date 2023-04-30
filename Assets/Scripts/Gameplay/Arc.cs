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
    [SerializeField] private Enemy enemy;
    public bool hasEnemy => ( enemy != null );
    [SerializeField] private Transform spotPlayerLeft;
    [SerializeField] private Transform spotPlayerRight;

    [Header("Enemy UI")]
    [SerializeField] private Transform enemyUI;
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private TextMeshProUGUI currentHealth;
    [SerializeField] private Image alertHealth;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private Image damageType;
    [SerializeField] private Image alertDamage;


    public void Initialize(){
        if (isVertical){
            enemyUI.Rotate(0,0,90);
            spotEnemy.Rotate(0,0,90);
            spotPlayerLeft.Rotate(0,0,90);
            spotPlayerRight.Rotate(0,0,90);
        }
    }

    public Node GetOtherNode(Node node){
        if (firstNode != node && secondNode != node) return null;
        return firstNode == node ? firstNode : secondNode;
    }

    public Transform GetClosestSpot(Node node){
        return ( Vector3.Distance(node.gameObject.transform.position, spotPlayerLeft.position) < Vector3.Distance(node.gameObject.transform.position, spotPlayerRight.position) ) ?
            spotPlayerLeft : spotPlayerRight;
    }
}
