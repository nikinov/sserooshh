using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveObject : MonoBehaviour
{
    public bool IsGrabbable { get; protected set; }
    public bool IsBox { get; protected set; }

    public interactiveObject()
    {
        IsGrabbable = true;
    }

    public virtual void Interact()
    {
        
    }
    public void EatMe()
    {
        DOTween.Init();
        GrabController _grabController = FindObjectOfType<GrabController>();
        StartCoroutine(wait(_grabController));
    }

    IEnumerator wait(GrabController _grabController)
    {
        if(_grabController._IsGrabbed)
            _grabController.DropItem();
        transform.DOScale(new Vector3(0, 0, 0), .7f);
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);
    }
}
