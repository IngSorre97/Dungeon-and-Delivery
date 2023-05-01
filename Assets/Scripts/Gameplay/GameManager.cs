using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool debug = true;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<ScriptableObject> enemiesPool;
    [SerializeField] private List<Item> availableLootItems;

    [SerializeField] private List<Graph> levels;
    private int currentLevel = 0;

    [SerializeField] private BattleAnimation battleAnimation;

    [SerializeField] private Camera mainCamera;

    public GameStates currentState {get; private set;}

    public delegate void StatsChange(Player player);
    static public StatsChange onStatsChanged;

    private MovesBuffer movesBuffer;
    private Node lastClicked;
    private Player player;
    private bool isMoving = false;
    public bool isResetting = false;

    public bool isPlaying = false;

    private Arc pendingArc = null;

    Graph.GraphData graphData;
    Graph playGraph;
    List<MovesBuffer.Move> storedMoves = null;



    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);

        movesBuffer = gameObject.AddComponent<MovesBuffer>();


        if (levels.Count == 0)
            levels.Add(Graph.GenerateGraph());

        StartGame();
    }

    private void StartGame(){
        movesBuffer.NewGame();

        GameObject playGraphObject = Instantiate(levels[currentLevel].gameObject);
        playGraph = playGraphObject.GetComponent<Graph>();
        playGraph.Initialize();

        graphData = playGraph.graphData;
        if (player != null){
            mainCamera.transform.SetParent(null);
            Destroy(player.gameObject);
            player = null;
        }

        isMoving = false;
        isResetting = false;
        isPlaying = false;

        GameObject playerObject = Instantiate(playerPrefab, graphData.startingNode.transform.position, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
        player.Initialize(graphData.startingNode);
        onStatsChanged?.Invoke(player);

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

        bool isValid = false, isRight = false;
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
                isRight = arc.isRight();
                break;
            }
        }
        if (!isValid)  {
            if (debug) Debug.Log("Movement unaccepted, not an adjacent node");
            return;
        }
        movesBuffer.Add(targetArc, clickedNode, isRight);
        lastClicked.SetLastClicked(false);
        clickedNode.SetLastClicked(true);
        lastClicked = clickedNode;
    }

    public void OnPlayClicked(){
        if (isPlaying) return;
        mainCamera.transform.SetParent(player.transform);
        mainCamera.transform.localPosition = new Vector3(0,0,-10);
        isPlaying = true;
        lastClicked.SetLastClicked(false);
        storedMoves = movesBuffer.storedMoves;
        MovementManager.Instance.StartMovement(player, storedMoves[0]);
    }

    public void NodeCheck(){
        if (playGraph.IsEndingNode(player.currentNode)){
            UIManager.Instance.Victory();
            return;
        }

        if (storedMoves.Count == 0){
            isMoving = false;
            UIManager.Instance.GameOver();
            return;
        }

        if (!player.currentNode)
        {
            PlayNextMove();
            return;
        }
        Loot currentLoot = player.currentNode.getLoot();
        if (currentLoot)
        {
            UIManager.Instance.OpenLootChoice(currentLoot);
        }
        else
        {
            PlayNextMove();
        }
        
    }

    public void PlayNextMove(){
        storedMoves.RemoveAt(0);
        if (storedMoves.Count > 0){
            if (debug) Debug.Log("New move to be played");
            MovementManager.Instance.StartMovement(player, storedMoves[0]);
            isMoving = true;
        } else {
            isMoving = false;
            UIManager.Instance.GameOver();
        }
    }

 

    public void OnResetClicked(){
        if (isPlaying) return;
        isResetting = true;
        movesBuffer.Reset();
    }

    public void FinishResetting(){
        isResetting = false;
        lastClicked = graphData.startingNode;
        graphData.startingNode.SetLastClicked(true);
    }

    public void OnUndoClicked(){
        if (isPlaying) return;
        lastClicked = movesBuffer.Undo();
        if (lastClicked == null){
            lastClicked = graphData.startingNode;
            graphData.startingNode.SetLastClicked(true);
        } else
            lastClicked.SetLastClicked(true);
    }

    public void StartBattle(Arc arc){
        Enemy enemy = arc.getEnemy;
        int enemyHealth = enemy.currentHealth;
        int minEnemy = enemy.minDamage;
        int maxEnemy = enemy.maxDamage;
        
        int playerHealth = player.currentHealth;
        int minPlayer = player.minDamage;
        int maxPlayer = player.maxDamage;

        List<BattleAnimation.BattleMove> battleMoves = new List<BattleAnimation.BattleMove>();
        bool isPlayerTurn = true;
        int damage;

        while (enemyHealth > 0 && playerHealth > 0){
            if (isPlayerTurn){
                damage = Random.Range(minPlayer, maxPlayer);
                enemyHealth -= damage;
            } else {
                damage = Random.Range(minEnemy, maxEnemy);
                playerHealth -= damage;
            }
            if (debug) Debug.Log($"Added a new move: it's {(isPlayerTurn ? "player" : "enemy")} turn and dealt {damage} points of damage");
            battleMoves.Add(new BattleAnimation.BattleMove(isPlayerTurn, damage));
            isPlayerTurn = !isPlayerTurn;
        }
        battleAnimation.gameObject.SetActive(true);
        if (debug) Debug.Log($"Battle outcome is: enemy has {enemyHealth} and player {playerHealth}");
        battleAnimation.StartBattle(battleMoves, player, enemy, playerHealth > 0);
        
        if (playerHealth > 0){
            player.currentHealth = playerHealth;
            pendingArc = arc;
            if (debug) Debug.Log("Player survived this battle");
        } else {
            if (debug) Debug.Log("Player perished in this battle");
        }
    }

    public void DeleteEnemy(){
        pendingArc.enemy.GetComponent<Animator>().SetTrigger("Death");
        pendingArc.enemy = null;
    }

    public void FinishedBattle(){
        onStatsChanged?.Invoke(player);
        NodeCheck();
    }

    public void GameOver(){
        UIManager.Instance.GameOver();
    }

    public void OnRetryClicked(){
        Destroy(playGraph.gameObject);
        StartGame();
    }

    public void OnNextClicked(){
        currentLevel = Mathf.Max(levels.Count - 1, currentLevel + 1);
        OnRetryClicked();
    }

    public void EquipItem(Item item)
    {
        player.EquipItem(item);
    }

    public void ConsumeItem(Item item)
    {
        player.ConsumeItem(item);
    }

}
