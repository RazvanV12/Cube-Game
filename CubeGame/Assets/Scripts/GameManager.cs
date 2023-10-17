using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentScore;
    public int highScore;

    public int currentLevel = 0;
    public int unlockedLevel;

    public Rect timerRect;
    public Rect coinRect;

    public float startTime;
    private string currentTime;

    private int coinsCollected;
    private int coinsTotal = 2;

    public GUISkin skin;
    
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    
    // setter and getter for coinsCollected
    public int CoinsCollected
    {
        get { return coinsCollected; }
        set { coinsCollected = value; }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        startTime -= Time.deltaTime;
        currentTime = string.Format("{0:0.0}", startTime);
        if (startTime <= 0)
        {
            startTime = 0;
            Destroy(gameObject);
            SceneManager.LoadScene("Main_Menu");
        }
    }

    private void Start()
    {
    }

    public void CompleteLevel()
    {
        if (currentLevel < 3)
        {
            currentLevel += 1;
            PlayerPrefs.SetInt("levelReached", currentLevel);
            PlayerPrefs.SetInt("Level " + currentLevel.ToString() + " score", currentScore);
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }
    
    public void AddCoin()
    {
        coinsCollected ++;
    }

    void OnGUI()
    {
        GUI.skin = skin;
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "Main_Menu")
        {
            if (startTime < 5f)
            {
                skin.GetStyle("Timer").normal.textColor = Color.red;
            }
            else
            {
                skin.GetStyle("Timer").normal.textColor = Color.white;
            }

            GUI.Label(timerRect, currentTime, skin.GetStyle("Timer"));
            GUI.Label(coinRect, coinsCollected.ToString() + "/" + coinsTotal.ToString(), skin.GetStyle("Coin"));
        }
    }
}
