using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class BattleAnimation : MonoBehaviour
{
    public struct BattleMove {
        public bool isPlayerTurn;
        public int damage;
        public BattleMove(bool isPlayerTurn, int damage){
            this.isPlayerTurn = isPlayerTurn;
            this.damage = damage;
        }
    }
    [SerializeField] private TextMeshProUGUI enemyHealth;
    [SerializeField] private TextMeshProUGUI playerHealth;

    [SerializeField] private Transform spotEnemy;
    [SerializeField] private Transform spotPlayer;

    PlayerController controller;

    GameObject enemyObject;
    GameObject playerObject;

    Coroutine battleCoroutine;

    void Start(){
        controller = new PlayerController();
        controller.Enable();
        controller.Battle.Skip.performed += (ctx) => EndBattle();
    }

    public void StartBattle(List<BattleMove> battleMoves, Player dummyPlayer, Enemy dummyEnemy){
        battleCoroutine = StartCoroutine(PlaySequence(battleMoves, dummyPlayer, dummyEnemy));
    }

    private IEnumerator PlaySequence(List<BattleMove> moves, Player dummyPlayer, Enemy dummyEnemy){
        enemyObject = Instantiate(dummyEnemy.gameObject, spotEnemy);
        enemyObject.transform.localPosition = Vector3.zero;
        enemyHealth.text = dummyEnemy.currentHealth.ToString(); 
        int enemyCurrentHealth = dummyEnemy.currentHealth;

        playerObject = Instantiate(dummyPlayer.gameObject, spotPlayer);
        playerObject.transform.localPosition = Vector3.zero;
        playerHealth.text = dummyPlayer.currentHealth.ToString(); 
        int playerCurrentHealt = dummyPlayer.currentHealth;
        yield return new WaitForSeconds(1.0f);

        while(moves.Count > 0){
            BattleMove move = moves[0];
            if (move.isPlayerTurn){
                enemyCurrentHealth -= move.damage;
                enemyHealth.text = enemyCurrentHealth.ToString();
                enemyObject.GetComponent<Animator>().SetTrigger("Attack");
            } else {
                playerCurrentHealt -= move.damage;
                playerHealth.text = playerCurrentHealt.ToString();
            }
            moves.RemoveAt(0);
            yield return new WaitForSeconds(1.5f);
            if (enemyCurrentHealth <= 0)
                enemyObject.GetComponent<Animator>().SetTrigger("Death");
        }
        yield return new WaitForSeconds(1.5f);
        EndBattle();
    }

    private void EndBattle(){
        if (GameManager.Instance.debug) Debug.Log("Battle is now finished!");
        if (battleCoroutine != null) StopCoroutine(battleCoroutine);
        Destroy(enemyObject);
        Destroy(playerObject);
        MovementManager.Instance.FinishMovement();
        gameObject.SetActive(false);
        battleCoroutine = null;
    }
}

