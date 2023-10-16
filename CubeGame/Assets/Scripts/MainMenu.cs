using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin skin;
    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(10, 10, 400, 75), "Cube Game");
        if (GUI.Button(new Rect(10, 100, 100, 45), "Play"))
        {
            SceneManager.LoadScene("Level 1");
        }
        
        if (GUI.Button(new Rect(10, 155, 100, 45), "Quit"))
        {
            print("I'm quitting");
        }
        
    }
}
