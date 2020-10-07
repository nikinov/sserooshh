using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private GameObject SettingsPanelUI;
    [SerializeField] private GameObject PlayPanelUI;
    
    public delegate void OnSave();
    public event OnSave Save;
    
    public delegate void OnReset();
    public event OnReset Reset;
    private void Start()
    {
        SettingsPanelUI.SetActive(false);
    }

    public void OpenSettings()
    {
        SettingsPanelUI.SetActive(true);
        PlayPanelUI.SetActive(false);
    }

    public void CloseSettings()
    {
        SettingsPanelUI.SetActive(false);
        PlayPanelUI.SetActive(true);
    }

    public void SaveLayout()
    {
        if(Save != null)
            Save();
    }
    
    public void ResetLayout()
    {
        if(Reset != null)
            Reset();
    }
}
