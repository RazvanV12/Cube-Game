using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin skin;
    
    [SerializeField] private GameManager gameManager;
    
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(10, 10, 400, 75), "Cube Game");
        if(PlayerPrefs.GetInt("levelReached", 1) > 1)
            if (GUI.Button(new Rect(10, 100, 100, 45), "Continue"))
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("levelReached", 1));
            }
        if (GUI.Button(new Rect(10, 155, 100, 45), "New Game"))
        {
            SceneManager.LoadScene(0);
            gameManager.currentLevel = 0;
            gameManager.CoinsCollected = 0;
            gameManager.startTime = 15;
            PlayerPrefs.SetInt("levelReached", 1);
        }
        if (GUI.Button(new Rect(10, 210, 100, 45), "Quit"))
        {
            print("I'm quitting");
        }
        
    }
}
