using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShipController : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Rotate(new Vector3(20,0,0) * Time.deltaTime);
    }
}
