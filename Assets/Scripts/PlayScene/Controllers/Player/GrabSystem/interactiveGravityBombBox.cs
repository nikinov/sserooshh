using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveGravityBombBox : interactiveObject
{
    private GrabController _grabController;
    private void Start()
    {
        DOTween.Init();
        _grabController = FindObjectOfType<GrabController>();
    }
    public interactiveGravityBombBox()
    {
        IsGrabbable = true;
        IsBox = true;
    }

    public override void Interact()
    {
        
    }
}
