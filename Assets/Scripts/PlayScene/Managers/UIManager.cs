using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private GameObject SettingsPanelUI;
    [SerializeField] private GameObject PlayPanelUI;
    [SerializeField] private TextMeshProUGUI WorningUI;
    
    public delegate void OnSave();
    public event OnSave Save;
    
    public delegate void OnReset();
    public event OnReset Reset;
    private void Start()
    {
        SettingsPanelUI.SetActive(false);
        WorningUI.gameObject.SetActive(false);
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

    public void DisplayWorning(String WarningText)
    {
        StartCoroutine(WaitForDisplayWorning(WarningText));
    }

    IEnumerator WaitForDisplayWorning(String Worningtext)
    {
        WorningUI.gameObject.SetActive(true);
        WorningUI.text = Worningtext;
        yield return new WaitForSeconds(2f);
        WorningUI.gameObject.SetActive(false);
    }
}
