using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseNavigation : MonoBehaviour
{
    [SerializeField] private float edgeSize = 30f;
    [SerializeField] private float moveAmount = 100f;
    [SerializeField] private Camera mainCamera;

    private void Start(){
        if (mainCamera == null) 
        mainCamera = gameObject.GetComponent<Camera>();

        if (mainCamera == null)
        {
            Debug.LogError("No camera component found, even on the same GameObject!");
            Destroy(this);
        }
    }

    private void Update(){
        if (GameManager.Instance.isPlaying) return;
        Vector3 cameraPosition = mainCamera.transform.position;
        if (Input.mousePosition.x > Screen.width - edgeSize){
            cameraPosition.x += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.x < edgeSize){
            cameraPosition.x -= moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y > Screen.height - edgeSize){
            cameraPosition.y += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y < edgeSize){
            cameraPosition.y -= moveAmount * Time.deltaTime;
        }

        mainCamera.transform.position = cameraPosition;

    }
    
}
