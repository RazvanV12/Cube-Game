using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //Player Stats Variables
    public float currentScore;
    [SerializeField] private int coinsCollected;
    [SerializeField] private int [] coinsTotal_Level = {2, 2, 3};
    
    // Game Info Variables
    public int [] levelTimers = {15, 10, 20};
    public float startTime;
    private string currentTime;
    
    public int [] levelScores = new int[3];
    public int currentLevel = 0;
    public int unlockedLevel;
    // Rect Variables
    public Rect timerRect;
    public Rect coinRect = new Rect();
    //GUI Skin
    public GUISkin skin;
    
    //Instance variable
    private static GameManager instance;
    
    // Functions

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

        startTime = levelTimers[0];
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
        startTime = levelTimers[currentLevel];
    }

    public void CompleteLevel()
    {
        currentScore = coinsCollected * 5 + startTime;
        if (currentScore > PlayerPrefs.GetFloat("Level " + currentLevel.ToString() + "highscore", 0))
            PlayerPrefs.SetFloat("Level " + currentLevel.ToString() + "highscore", currentScore);
        if (currentLevel < 2)
        {
            currentLevel ++;
            startTime = levelTimers[currentLevel];
            coinsCollected = 0;
            PlayerPrefs.SetInt("levelUnlocked", currentLevel);
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

            coinRect = new Rect(Screen.width - Screen.width * 0.1f, 25, 20, 200);
            GUI.Label(timerRect, currentTime, skin.GetStyle("Timer"));
            if(currentLevel < 3) 
                GUI.Label(coinRect, coinsCollected.ToString() + "/" + coinsTotal_Level[currentLevel].ToString(), skin.GetStyle("Coin"));
        }
    }
}

