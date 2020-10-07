using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveBombBox : interactiveObject
{
    private HealthSystem _healthSystem;
    private GrabController _grabController;
    private void Start()
    {
        DOTween.Init();
        _grabController = FindObjectOfType<GrabController>();
    }
    public interactiveBombBox()
    {
        IsGrabbable = true;
    }

    public override void Interact()
    {
        
    }
    
    public void EatMe()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        if(_grabController._IsGrabbed)
            _grabController.DropItem();
        transform.DOScale(new Vector3(0, 0, 0), .7f);
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);
    }
}

