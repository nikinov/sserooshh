using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabed : MonoBehaviour
{
    public delegate void ColInAction();
    public event ColInAction OnColIn;
    public delegate void ColOutAction();
    public event ColOutAction OnColOut;
    private void OnTriggerEnter(Collider other)
    {
        OnColIn();
    }

    private void OnTriggerExit(Collider other)
    {
        OnColOut();
    }
}
