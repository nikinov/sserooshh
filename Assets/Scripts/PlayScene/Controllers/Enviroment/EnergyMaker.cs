using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnergyMaker : MonoBehaviour
{
    [SerializeField] private GameObject Charger;
    [SerializeField] private GameObject Bomb;
    [SerializeField] private Transform tr;
    private bool isMelting;
    private GrabController _grabController;

    private void Awake()
    {
        DOTween.Init();
        _grabController = FindObjectOfType<GrabController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_grabController._IsGrabbed && !isMelting)
        {
            if (other.gameObject.GetComponent<interactiveEnergyBox>() != null)
            {
                other.gameObject.GetComponent<interactiveEnergyBox>().EatMe();
                GameObject go = Instantiate(Charger, tr.position, tr.rotation, tr);
                go.transform.localScale = Vector3.zero;
                go.transform.DOScale(new Vector3(1, 1, 1), 1);
                StartCoroutine(wait());
            }
            else if(other.gameObject.GetComponent<interactiveBombBox>() != null)
            {
                other.gameObject.GetComponent<interactiveBombBox>().EatMe();
                GameObject go = Instantiate(Bomb, tr.position, tr.rotation, tr);
                go.transform.localScale = Vector3.zero;
                go.transform.DOScale(new Vector3(1, 1, 1), 1);
                StartCoroutine(wait());
                
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
