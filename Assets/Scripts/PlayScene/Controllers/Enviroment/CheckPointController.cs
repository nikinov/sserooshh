using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [SerializeField] private int CheckPointNum;
    private SaveLoadManager _saveLoadManager;
    private Player player;
    private Transform PlayerSpawn;

    private void Awake()
    {
        _saveLoadManager = FindObjectOfType<SaveLoadManager>();
        player = FindObjectOfType<Player>();
        _saveLoadManager.LoadCheckPont += SpawnPlayer;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            _saveLoadManager.EnterCheckPoint(CheckPointNum);
        }
    }

    private void SpawnPlayer(int CheckPointNumber)
    {
        if (CheckPointNumber == CheckPointNum)
        {
            foreach (Transform t in gameObject.GetComponentInChildren<Transform>())
            {
                player.gameObject.transform.position = t.position;
            }
        }
    }
}
