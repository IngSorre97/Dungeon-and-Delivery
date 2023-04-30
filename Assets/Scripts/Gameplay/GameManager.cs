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

    public delegate void StatsChange(Player player);
    static public StatsChange onStatsChanged;

    private MovesBuffer movesBuffer;
    private Node lastClicked;
    private Player player;
    private bool isMoving = false;
    public bool isResetting = false;

    Graph.GraphData graphData;
    List<MovesBuffer.Move> storedMoves = null;

    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);

        movesBuffer = gameObject.AddComponent<MovesBuffer>();

        if (playGraph == null)
            playGraph = Graph.GenerateGraph();

        playGraph.Initialize();

        graphData = playGraph.graphData;
        GameObject playerObject = Instantiate(playerPrefab, graphData.startingNode.transform.position, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
        player.Initialize(graphData.startingNode);

        currentState = GameStates.Playing;
        lastClicked = graphData.startingNode;
        lastClicked.SetLastClicked(true);
    }

    public void OnMovementInput(MovementType movement){
        Node targetNode = lastClicked.isMovementPossible(movement);
        if (targetNode != null)
            OnNodeClicked(targetNode);
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

        if (isResetting) {
            if (debug) Debug.Log("Movement unaccepted, buffer is resetting");
            return;
        }

        if (lastClicked != null && lastClicked == clickedNode) return;

        bool isValid = false;
        Arc targetArc = null;
        foreach(Arc arc in lastClicked.AdjacentArcs()){
            Node otherNode = arc.GetOtherNode(lastClicked);
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

        movesBuffer.Add(targetArc, clickedNode);
        lastClicked.SetLastClicked(false);
        clickedNode.SetLastClicked(true);
        lastClicked = clickedNode;
        //isMoving = true;
        //MovementManager.Instance.StartMovement(player, clickedNode, targetArc);
    }

    public void OnPlayClicked(){
        lastClicked.SetLastClicked(false);
        storedMoves = movesBuffer.storedMoves;
        MovementManager.Instance.StartMovement(player, storedMoves[0]);
    }

    public void PlayNextMove(){
        storedMoves.RemoveAt(0);
        if (storedMoves.Count > 0){
            MovementManager.Instance.StartMovement(player, storedMoves[0]);
            isMoving = true;
        } else {
            isMoving = false;
        }
    }

    public void OnResetClicked(){
        isResetting = true;
        movesBuffer.Reset();
    }

    public void FinishResetting(){
        isResetting = false;
        lastClicked = graphData.startingNode;
        graphData.startingNode.SetLastClicked(true);
    }

    public void OnUndoClicked(){
        lastClicked = movesBuffer.Undo();
        if (lastClicked == null){
            lastClicked = graphData.startingNode;
            graphData.startingNode.SetLastClicked(true);
        } else
            lastClicked.SetLastClicked(true);
    }
}
