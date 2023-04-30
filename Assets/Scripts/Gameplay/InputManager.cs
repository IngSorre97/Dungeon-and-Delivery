using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController controller;

    void Start(){
        controller = new PlayerController();
        controller.Enable();

        controller.Movement.Up.performed += (ctx) => GameManager.Instance?.OnMovementInput(MovementType.Up);
        controller.Movement.Right.performed += (ctx) => GameManager.Instance?.OnMovementInput(MovementType.Right);
        controller.Movement.Down.performed += (ctx) => GameManager.Instance?.OnMovementInput(MovementType.Down);
        controller.Movement.Left.performed += (ctx) => GameManager.Instance?.OnMovementInput(MovementType.Left);
    }
}
