using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance;

    [SerializeField] private float movementDuration = 1.0f;
    void Start(){
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void StartMovement(Player player, Node destinationNode, Arc arc){
        Transform destination = arc.hasEnemy ? arc.GetClosestSpot(destinationNode) : destinationNode.transform;
        player.SetNewDestination(arc.hasEnemy ? null : destinationNode);
        StartCoroutine(MoveCoroutine(player, destination));
    }

    private IEnumerator MoveCoroutine(Player player, Transform destination){
        float time = 0;
        Vector3 startPosition = player.gameObject.transform.position;
        while (time < movementDuration){
            time += Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPosition, destination.position, time/movementDuration);
            yield return null;
        }
        GameManager.Instance.EndMovement();
        yield return null;
    }
}
