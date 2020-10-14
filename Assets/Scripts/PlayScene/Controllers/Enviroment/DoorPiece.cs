using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorPiece : MonoBehaviour
{
    public GameObject NormalDoor;
    [HideInInspector] public bool WasActivated;
    [SerializeField] private Transform parent;
    private Vector3 OriginalPossition;
    private Vector3 OriginalRotation;
    private string GravityBombTag = "GravityBomb";
    
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
        if (other.gameObject.tag == GravityBombTag)
        {
            if (!WasActivated)
            {
                foreach (Transform child in parent)
                {
                    StartCoroutine(child.GetComponent<DoorPiece>().wait());
                }
                StartCoroutine(waitMaster(other.gameObject));
            }
        }
    }

    public IEnumerator wait()
    {
        WasActivated = true;
        transform.DOMove(OriginalPossition, 1);
        transform.DORotate(OriginalRotation, 1);
        yield return new WaitForSeconds(1);
    }
    public IEnumerator waitMaster(GameObject other)
    {
        other.transform.DOScale(Vector3.zero, .75f);
        yield return new WaitForSeconds(1);
        Instantiate(NormalDoor, transform.parent.parent);
        Destroy(other);
        Destroy(parent.gameObject);
    }
}
