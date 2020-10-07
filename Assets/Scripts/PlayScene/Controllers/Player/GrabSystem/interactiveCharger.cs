using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveCharger : interactiveObject
{
    private HealthSystem _healthSystem;
    private void Start()
    {
        _healthSystem = FindObjectOfType<HealthSystem>();
        DOTween.Init();
    }

    public interactiveCharger()
    {
        IsGrabbable = false;
    }

    public override void Interact()
    {
        _healthSystem.StartCharging();
        StartCoroutine(recharge());
    }

    IEnumerator recharge()
    {
        gameObject.GetComponent<MeshRenderer>().material.DOColor(Color.black, 1);
        transform.DOScale(new Vector3(0,0,0), 1);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
