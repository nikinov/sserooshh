using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TopPiece : MonoBehaviour
{
    [HideInInspector] public bool WasActivated;
    private GameObject NormalDoor;
    private Vector3 OriginalPossition;
    private Vector3 OriginalRotation;
    private string UnboxerLava = "UnboxerLava";
    
    private void Awake()
    {
        OriginalPossition = new Vector3();
        OriginalRotation = new Vector3();
        WasActivated = false;
        OriginalPossition = transform.position;
        OriginalRotation = transform.rotation.eulerAngles;
        DOTween.Init();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == UnboxerLava)
        {
            if (!WasActivated)
            {
                foreach (Transform child in gameObject.transform.parent)
                {
                    StartCoroutine(child.GetComponent<TopPiece>().wait());
                }
                StartCoroutine(waitMaster());
            }
        }
    }

    public IEnumerator wait()
    {
        WasActivated = true;
        yield return new WaitForSeconds(1);
    }
    public IEnumerator waitMaster()
    {
        yield return new WaitForSeconds(.05f);
        transform.parent.parent.GetChild(0).gameObject.SetActive(true);
        transform.parent.parent.GetChild(0).DOLocalMoveZ(0, 0);
        transform.parent.parent.GetChild(0).DOLocalMoveZ(2, 1);
        Destroy(transform.parent.gameObject);
    }
}
