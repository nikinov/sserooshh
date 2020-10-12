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
        IsBox = true;
    }

    public override void Interact()
    {
        
    }
}

