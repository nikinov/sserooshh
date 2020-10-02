using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveCharger : interactiveObject
{
    private HealthSystem _healthSystem;
    private bool used;
    private void Start()
    {
        _healthSystem = FindObjectOfType<HealthSystem>();
        used = false;
        DOTween.Init();
    }

    public interactiveCharger()
    {
        IsGrabbable = false;
    }

    public override void Interact()
    {
        if (!used)
        {
            _healthSystem.StartCharging();
            used = true;
            gameObject.GetComponent<MeshRenderer>().material.DOColor(Color.black, 1);
            StartCoroutine(recharge());
        }
    }

    IEnumerator recharge()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<MeshRenderer>().material.DOColor(Color.yellow, 10);
        yield return new WaitForSeconds(10);
        used = false;
    }
}
