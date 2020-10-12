using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MakerTop : MonoBehaviour
{
    [SerializeField] private GameObject PlaceHolder2;
    [SerializeField] private GameObject PlaceHolder3;
    [SerializeField] private GameObject PlaceHolder4;
    private bool isOnIt;
    private bool isShowing;
    private GameManager _gameManager;
    private GameObject CurrentlyShowingObject;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        DOTween.Init();
        isShowing = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isShowing)
        {
            if (other.gameObject.GetComponent<interactiveObject>() != null)
            {
                print("ddd");
                StartCoroutine(CheckForStay(other.gameObject));
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        print("bebe");
        if (isShowing)
        {
            if (isOnIt)
            {
                StartCoroutine(waitForOnIt(other.gameObject));
            }
            else
            {
                if (other.gameObject.name == _gameManager.UnboxInteractivesNames[_gameManager.InteractivesNames.IndexOf(CurrentlyShowingObject.name.Replace("(Clone)", ""))])
                {
                    isShowing = false;
                    StartCoroutine(ShowEndWait(CurrentlyShowingObject, PlaceHolder2));
                }
            }
        }
    }

    public void EnterTheUnboxer()
    {
        StartCoroutine(ShowEndWait(CurrentlyShowingObject, PlaceHolder3));
        Instantiate(_gameManager.FracturedUnboxerTop, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
        gameObject.SetActive(false);
    }

    private void DisplayItem(GameObject objectBoxForDisplay)
    {
        CurrentlyShowingObject = Instantiate(_gameManager.Interactives[_gameManager.UnboxInteractivesNames.IndexOf(objectBoxForDisplay.name)], PlaceHolder2.transform.position, PlaceHolder2.transform.rotation, PlaceHolder2.transform);
        StartCoroutine(ShowStartWait(CurrentlyShowingObject));
    }

    IEnumerator CheckForStay(GameObject other)
    {
        isOnIt = true;
        isShowing = true;
        interactiveObject interactive = other.gameObject.GetComponent<interactiveObject>();
        if (interactive != null)
        {
            if (interactive.IsBox)
            {
                DisplayItem(other.gameObject);
            }
        }
        print("aaa");
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator ShowStartWait(GameObject go)
    {
        go.transform.localScale = Vector3.zero;
        go.transform.DOMove(PlaceHolder4.transform.position, .5f);
        go.transform.DORotate(PlaceHolder4.transform.rotation.eulerAngles, .5f);
        go.transform.DOScale(new Vector3(.4f, .4f, .4f), .5f);
        Destroy(go.GetComponent<Rigidbody>());
        Destroy(go.GetComponent<interactiveObject>());
        go.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(.5f);
        isOnIt = false;
    }

    IEnumerator ShowEndWait(GameObject go, GameObject PlaceHolder)
    {
        go.transform.DOMove(PlaceHolder.transform.position, .5f);
        go.transform.DORotate(PlaceHolder.transform.rotation.eulerAngles, .5f);
        go.transform.DOScale(Vector3.zero, .5f);
        yield return new WaitForSeconds(.5f);
        Destroy(go);
        CurrentlyShowingObject = null;
    }

    IEnumerator waitForOnIt(GameObject other)
    {
        yield return new WaitForSeconds(.5f);
        if (other.name == _gameManager.UnboxInteractivesNames[_gameManager.InteractivesNames.IndexOf(CurrentlyShowingObject.name.Replace("(Clone)", ""))])
        {
            isShowing = false;
            StartCoroutine(ShowEndWait(CurrentlyShowingObject, PlaceHolder2));
        }
    }
}

















