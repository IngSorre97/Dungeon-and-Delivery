using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance;

    [SerializeField] private float movementDuration = 1.0f;
    [SerializeField] private BattleAnimation battleAnimationPrefab;

    private Transform pendingDestination;
    private Player pendingPlayer;

    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void StartMovement(Player player, MovesBuffer.Move move){
        bool hasEnemy = move.arc.hasEnemy;
        Transform destination = hasEnemy ? move.arc.GetClosestSpot(move.node) : move.node.transform;
        player.SetNewDestination(hasEnemy ? null : move.node, move.isRight);
        StartCoroutine(MoveCoroutine(player, destination, move.arc, move.node));
    }

    private IEnumerator MoveCoroutine(Player player, Transform destination, Arc arc, Node node){
        float time = 0;
        Vector3 startPosition = player.transform.position;
        while (time < movementDuration){
            time += Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPosition, destination.position, time/movementDuration);
            yield return null;
        }

        if (arc.hasEnemy){
            pendingDestination = node.transform;
            pendingPlayer = player;
            GameManager.Instance.StartBattle(arc);
        } else {
            GameManager.Instance.NodeCheck();
        }
        yield return null;
    }

    public void FinishMovement(){
        GameManager.Instance.DeleteEnemy();
        StartCoroutine(FinishedBattle());
    }

    public IEnumerator FinishedBattle(){
        if (GameManager.Instance.debug) Debug.Log("Pending movement coroutine is now going");
        float time = 0;
        Vector3 startPosition = pendingPlayer.transform.position;
        while (time < movementDuration){
            time += Time.deltaTime;
            pendingPlayer.transform.position = Vector3.Lerp(startPosition, pendingDestination.position, time/movementDuration);
            yield return null;
        }
        if (GameManager.Instance.debug) Debug.Log("Pending movement coroutine is now completed");
        GameManager.Instance.FinishedBattle();
        yield return null;
    }


}
