using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool debug = true;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<ScriptableObject> enemiesPool;

    [SerializeField] private Graph playGraph;

    public GameStates currentState {get; private set;}

    public delegate void StateChange(GameStates gamestate);
    static public StateChange onStateChanged;

    public delegate void StatsChange(Player player);
    static public StatsChange onStatsChanged;

    private Player player;
    private bool isMoving = false;

    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);


        if (playGraph == null)
            playGraph = Graph.GenerateGraph();

        playGraph.Initialize();

        Graph.GraphData graphData = playGraph.graphData;
        GameObject playerObject = Instantiate(playerPrefab, graphData.startingNode.transform.position, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
        player.Initialize(graphData.startingNode);

        currentState = GameStates.Playing;
    }

    public void OnNodeClicked(Node clickedNode){

        if (debug){
            Debug.Log("Current node is now " + player.currentNode.name);
            Debug.Log("The node " + clickedNode.name + " has been clicked");
        } 

        if (currentState != GameStates.Playing) {
            if (debug) Debug.Log("Movement unaccepted, not playing state");
            return;
        }

        if (isMoving) {
            if (debug) Debug.Log("Movement unaccepted, something is moving");
            return;
        }

        bool isValid = false;
        Arc targetArc = null;
        foreach(Arc arc in player.currentNode.adjacentArcs){
            Node otherNode = arc.GetOtherNode(clickedNode);
            if (otherNode == null){
                if (debug) Debug.LogWarning($"Attention! The arc {arc.name} is an adjacent arc, but none of its nodes are the starting one!");
                continue;
            }
            if (debug) Debug.Log(otherNode.name + " is an adjacent node");
            if (otherNode == clickedNode)
            {
                isValid = true;
                targetArc = arc;
                break;
            }
        }
        if (!isValid)  {
            if (debug) Debug.Log("Movement unaccepted, not an adjacent node");
            return;
        }
        isMoving = true;
        MovementManager.Instance.StartMovement(player, clickedNode, targetArc);
    }

    public void EndMovement(){
        if (!isMoving)
            Debug.LogWarning("Attention! EndMovement called but in a non moving environment");
        isMoving = false;
    }


}
