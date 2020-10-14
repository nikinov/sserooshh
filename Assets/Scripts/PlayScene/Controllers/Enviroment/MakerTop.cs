using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MakerTop : MonoBehaviour
{
    public bool isShowing;
    [SerializeField] private GameObject PlaceHolder2;
    [SerializeField] private GameObject PlaceHolder3;
    [SerializeField] private GameObject PlaceHolder4;
    private bool isOnIt;
    private GameManager _gameManager;
    private GameObject CurrentlyShowingObject;
    public delegate void ShowingAction();
    public event ShowingAction OnShowing;
    public delegate void StopShowingAction();
    public event StopShowingAction OnStopShowing;
    [HideInInspector] public int ObjectsOnTop;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        DOTween.Init();
        isShowing = false;
        ObjectsOnTop = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        ObjectsOnTop += 1;
        if (!isShowing)
        {
            if (other.gameObject.GetComponent<interactiveObject>() != null)
            {
                StartCoroutine(CheckForStay(other.gameObject));
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (ObjectsOnTop == 1)
        {
            ObjectsOnTop -= 1;
            if (isShowing)
            {
                if (isOnIt)
                {
                    StartCoroutine(waitForOnIt(other.gameObject));
                }
                else
                {
                    if(CurrentlyShowingObject != null)
                    {
                        isShowing = false;
                        OnStopShowing();
                        StartCoroutine(ShowEndWait(CurrentlyShowingObject, PlaceHolder2));
                    }
                }
            }
        }
        else
        {
            ObjectsOnTop -= 1;
        }
    }

    public void EnterTheUnboxer()
    {
        StartCoroutine(ShowEndWait(CurrentlyShowingObject, PlaceHolder3));
        Instantiate(_gameManager.FracturedUnboxerTop, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
        ObjectsOnTop -= 1;
        gameObject.SetActive(false);
    }

    private void DisplayItem(GameObject objectBoxForDisplay)
    {
        CurrentlyShowingObject = Instantiate(_gameManager.Interactives[_gameManager.UnboxInteractivesNames.IndexOf(objectBoxForDisplay.name)], PlaceHolder2.transform.position, PlaceHolder2.transform.rotation, PlaceHolder2.transform);
        StartCoroutine(ShowStartWait(CurrentlyShowingObject));
        OnShowing();
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
        if (_gameManager.UnboxInteractivesNames[
            _gameManager.InteractivesNames.IndexOf(CurrentlyShowingObject.name.Replace("(Clone)", ""))] != null)
        {
            if (other.name == _gameManager.UnboxInteractivesNames[_gameManager.InteractivesNames.IndexOf(CurrentlyShowingObject.name.Replace("(Clone)", ""))])
            {
                isShowing = false;
                StartCoroutine(ShowEndWait(CurrentlyShowingObject, PlaceHolder2));
            }
        }
    }
}











