using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveEnergyBox : interactiveObject
{
    private void Start()
    {
        DOTween.Init();
    }

    public interactiveEnergyBox()
    {
        IsGrabbable = true;
        IsBox = true;
    }

    public override void Interact()
    {
        
    }
}
