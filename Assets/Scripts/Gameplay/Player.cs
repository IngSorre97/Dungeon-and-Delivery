using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Node currentNode {get; private set;}
    public Weapon currentWeapon {get; private set;}

    public void Initialize(Node startingNode){
        currentNode = startingNode;
    }

    public void SetNewDestination(Node destinationNode){
        currentNode = destinationNode;
    }
}
