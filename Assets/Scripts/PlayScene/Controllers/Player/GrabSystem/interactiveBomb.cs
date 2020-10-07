using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveBomb : interactiveObject
{
    [SerializeField] private GameObject ExplosionEffect;
    private Rigidbody rb;
    private bool CheckForExplosion;

    private void Awake()
    {
        ExplosionEffect.SetActive(false);
        rb = GetComponent<Rigidbody>();
        DOTween.Init();
        StartCoroutine(WaitForStarting());
    }

    public interactiveBomb()
    {
        IsGrabbable = true;
    }

    public override void Interact()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (rb.velocity.magnitude > 2 && !CheckForExplosion)
        {
            transform.DOScale(new Vector3(0, 0, 0), .6f);
            CheckForExplosion = true;
            ExplosionEffect.SetActive(true);
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    IEnumerator WaitForStarting()
    {
        CheckForExplosion = true;
        yield return new WaitForSeconds(2);
        CheckForExplosion = false;
    }
}
