using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
