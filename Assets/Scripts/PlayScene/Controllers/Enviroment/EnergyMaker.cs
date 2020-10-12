using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class EnergyMaker : MonoBehaviour
{
    [SerializeField] private Transform SpawnPlaceholder;
    private bool isMelting;
    private GrabController _grabController;
    private GameManager _gameManager;

    private void Awake()
    {
        DOTween.Init();
        _grabController = FindObjectOfType<GrabController>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_grabController._IsGrabbed && !isMelting)
        {
            if (other.gameObject.GetComponent<interactiveObject>() != null)
            {
                if (other.gameObject.GetComponent<interactiveObject>().IsBox)
                {
                    other.gameObject.GetComponent<interactiveObject>().EatMe();
                    GameObject go = Instantiate(_gameManager.Interactives[_gameManager.UnboxInteractivesNames.IndexOf(other.gameObject.GetComponent<interactiveObject>().gameObject.name.Replace("(Clone)", ""))], SpawnPlaceholder.position, SpawnPlaceholder.rotation, SpawnPlaceholder);
                    go.transform.localScale = Vector3.zero;
                    go.transform.DOScale(new Vector3(.75f, .75f, .75f), 1);
                    StartCoroutine(wait());
                }
            }
        }
    }

    IEnumerator wait()
    {
        isMelting = true;
        yield return new WaitForSeconds(1);
        isMelting = false;
    }
}
