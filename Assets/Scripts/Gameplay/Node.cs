using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    [SerializeField] private SpriteRenderer frame;
    [SerializeField] private GameObject counter;
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private Sprite background;
    [SerializeField] private Loot loot;
    [SerializeField] private bool isEndNode;
    [SerializeField] private Arc up;
    [SerializeField] private Arc right;
    [SerializeField] private Arc down;
    [SerializeField] private Arc left;

    [SerializeField] private List<int> indexes = new List<int>();

    public void OnMouseDown(){
        GameManager.Instance.OnNodeClicked(this);
    }

    public List<Arc> AdjacentArcs(){
        List<Arc> returnList = new List<Arc>();
        if (up != null) returnList.Add(up);
        if (right != null) returnList.Add(right);
        if (down != null) returnList.Add(down);
        if (left != null) returnList.Add(left);
        return returnList;
    }

    public Node isMovementPossible(MovementType movement){
        switch(movement){
            case MovementType.Up:
                if (up == null) return null;
                return up.GetOtherNode(this);
            case MovementType.Right:
                if (right == null) return null;
                return right.GetOtherNode(this);
            case MovementType.Down:
                if (down == null) return null;
                return down.GetOtherNode(this);
            case MovementType.Left:
                if (left == null) return null;
                return left.GetOtherNode(this);
            default:
                return null;
        }
    }

    public void SetCounter(int index){
        counter.SetActive(true);
        counterText.text = index.ToString();
        indexes.Add(index);
    }

    public void UndoMove(){
        frame.color = Color.black;
        if (indexes.Count == 0) DeleteCounter();
        else {
            indexes.RemoveAt(indexes.Count - 1);
            if (indexes.Count == 0) DeleteCounter();
            else
                counterText.text = indexes[indexes.Count - 1].ToString();
        }
    }

    public void DeleteCounter(){
        counter.SetActive(false);
    }

    public void SetLastClicked(bool highlight){
        frame.color = highlight ? Color.green : Color.black;
    }


}
