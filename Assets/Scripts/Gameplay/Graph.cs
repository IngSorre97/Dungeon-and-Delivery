using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public struct GraphData {
        public string name;
        public Node startingNode;
        public Vector3 bottomCorner;
        public Vector3 topCorner;
    }

    public GraphData graphData;
    [SerializeField] private Node startingNode;
    [SerializeField] private string graphName;
    [SerializeField] private Transform nodes;
    [SerializeField] private Transform arcs;
    [SerializeField] private List<Node> endingNodes;

    public static Graph GenerateGraph(){
        return new Graph();
    }

    public void Initialize(){
        InitializeNodes();
        InitializeArcs();

        graphData.name = graphName;
        graphData.startingNode = startingNode;
    }

    private void InitializeNodes(){

    }

    private void InitializeArcs(){
        
    }
}
