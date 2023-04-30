using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance;

    [SerializeField] private float movementDuration = 1.0f;
    [SerializeField] private BattleAnimation battleAnimationPrefab;

    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void StartMovement(Player player, MovesBuffer.Move move){
        bool hasEnemy = move.arc.hasEnemy;
        Transform destination = hasEnemy ? move.arc.GetClosestSpot(move.node) : move.node.transform;
        player.SetNewDestination(hasEnemy ? null : move.node);
        StartCoroutine(MoveCoroutine(player, destination, move.arc));
    }

    private IEnumerator MoveCoroutine(Player player, Transform destination, Arc arc){
        float time = 0;
        Vector3 startPosition = player.gameObject.transform.position;
        while (time < movementDuration){
            time += Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPosition, destination.position, time/movementDuration);
            yield return null;
        }

        if (arc.hasEnemy){
            UIManager.Instance.StartBattle(arc);
        } else {
            GameManager.Instance.PlayNextMove();
        }
        yield return null;
    }
}
