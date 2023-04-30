using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesBuffer : MonoBehaviour
{
    public struct Move{
        public Arc arc;
        public Node node;
        public int index;
        public Move(Arc arc, Node node, int index){
            this.arc = arc;
            this.node = node;
            this.index = index;
        }
    }
    public List<Move> storedMoves {get; private set;} = new List<Move>();
    private int index = 1;

    public void Add(Arc arc, Node node){
        storedMoves.Add(new Move(arc, node, index));
        node.SetCounter(index);
        index++;
    }

    public void Reset(){
        StartCoroutine(ClearBuffer());
    }

    private IEnumerator ClearBuffer(){
        for(int i=storedMoves.Count-1; i>=0; i--){
            storedMoves[i].node.UndoMove();
            storedMoves.Remove(storedMoves[i]);
            if (i > 1)
                storedMoves[i-1].node.SetLastClicked(true);
            yield return new WaitForSeconds(0.1f);
        }
                    
        storedMoves.Clear();
        index = 1;
        GameManager.Instance.FinishResetting();
        yield return null;
    }

    public Node Undo(){
        if (index == 1) return null;
        Move move = storedMoves[storedMoves.Count-1];
        move.node.UndoMove();
        index--;
        storedMoves.Remove(move);
        if (storedMoves.Count != 0){
            storedMoves[storedMoves.Count - 1].node.SetLastClicked(true);
            return storedMoves[storedMoves.Count - 1].node;
        }
        return null;
    }
}
