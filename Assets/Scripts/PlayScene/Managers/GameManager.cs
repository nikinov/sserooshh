using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> UnboxInteractives;
    public List<GameObject> Interactives;
    [HideInInspector] public List<string> UnboxInteractivesNames;
    [HideInInspector] public List<string> InteractivesNames;
    public GameObject FracturedUnboxerTop;

    private void Awake()
    {
        foreach (GameObject go in UnboxInteractives)
        {
            UnboxInteractivesNames.Add(go.name);
        }
        foreach (GameObject go in Interactives)
        {
            InteractivesNames.Add(go.name);
        }
    }
}
