using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public delegate void OnLoadCheckPoint(int CheckPoint);
    public event OnLoadCheckPoint LoadCheckPont;

    [SerializeField] private bool Editor;

    private int CurrentCheckPoint;
    
    // Player Prefs
    private string CheckPoints = "CheckPoint";
    private string PlayingForTheFirstTime = "PlayingForTheFirstTime";
    
    private void Awake()
    {
        if (Editor)
        {
            PlayerPrefs.SetString(PlayingForTheFirstTime, "editor");
        }
        else
        {
            if (PlayerPrefs.GetString(PlayingForTheFirstTime) != "no")
            {
                PlayerPrefs.SetString(PlayingForTheFirstTime, "yes");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initLoad();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initLoad()
    {
        if (PlayerPrefs.GetString(PlayingForTheFirstTime) == "yes")
        {
            PlayerPrefs.SetString(PlayingForTheFirstTime, "no");
            PlayerPrefs.SetInt(CheckPoints, 0);
            CurrentCheckPoint = 0;
            LoadCheck(0);
        }
        else if (PlayerPrefs.GetString(PlayingForTheFirstTime) == "no")
        {
            if (PlayerPrefs.GetInt(CheckPoints) != null)
                LoadCheck(PlayerPrefs.GetInt(CheckPoints));
            else
            {
                print("the player prefs are not set");
            }
        }
        else if (PlayerPrefs.GetString(PlayingForTheFirstTime) == "editor")
        {
            
        }
    }

    void LoadCheck(int CheckPoint)
    {
        if(LoadCheckPont != null)
            LoadCheckPont(CheckPoint);
        else
        {
            print("LoadCheckPoints is null");
        }
        
    }

    public void EnterCheckPoint(int CheckPoint)
    {
        if (CurrentCheckPoint != CheckPoint)
        {
            CurrentCheckPoint = CheckPoint;
            PlayerPrefs.SetInt(CheckPoints, CheckPoint);
        }
    }

}








