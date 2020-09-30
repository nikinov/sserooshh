using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactiveObject : MonoBehaviour
{
    public bool IsGrabbable { get; protected set; }

    public interactiveObject()
    {
        IsGrabbable = true;
    }

    public virtual void Interact()
    {
        
    }
}
