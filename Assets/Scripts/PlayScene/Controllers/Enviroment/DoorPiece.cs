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
                    print(child.name);
                    StartCoroutine(child.GetComponent<DoorPiece>().wait());
                }
                StartCoroutine(waitMaster());
            }
        }
    }

    public IEnumerator wait()
    {
        print("aa");
        WasActivated = true;
        transform.DOMove(OriginalPossition, 1);
        transform.DORotate(OriginalRotation, 1);
        yield return new WaitForSeconds(1);
    }
    public IEnumerator waitMaster()
    {
        yield return new WaitForSeconds(1);
        Instantiate(NormalDoor, FindObjectOfType<Room1>().transform);
        Destroy(parent.gameObject);
    }
}
