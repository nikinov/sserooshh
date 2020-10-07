using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    public GameObject BreakVersion;
    public GameObject ParentVersion;
    public float bForce;
    private int active;
    private GrabController _grabController;

    private void Start()
    {
        _grabController = FindObjectOfType<GrabController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > bForce && active == 0 && other.gameObject.tag == "CanDestroy" && !_grabController._IsGrabbed)
        {
            active+=1;
            GameObject go = Instantiate(BreakVersion, transform.position, transform.rotation, ParentVersion.transform);
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(10f, Vector3.zero, 0f);
            Destroy(gameObject);
        }
    }
}
