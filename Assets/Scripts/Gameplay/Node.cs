using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private Sprite background;
    [SerializeField] private Loot loot;
    [SerializeField] private bool isEndNode;
    [SerializeField] private List<Arc> arcs;
    public List<Arc> adjacentArcs => arcs;

    public void OnMouseDown(){
        GameManager.Instance.OnNodeClicked(this);

    }


}
