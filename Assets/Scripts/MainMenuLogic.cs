using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenuLogic : MonoBehaviour
{ 
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
