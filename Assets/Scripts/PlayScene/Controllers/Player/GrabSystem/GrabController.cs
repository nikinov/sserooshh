﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GrabController : MonoBehaviour
{
        [SerializeField] private KeyCode interactKey;
        [SerializeField] private KeyCode throwKey;
        [SerializeField] private float interactDistance = 7f;
        [SerializeField] private float grabbableCheck = 5f;
        [SerializeField] private float throwForce = 10;

        [SerializeField] private Transform cursor;
        [SerializeField] private float resizeCursor;
        [SerializeField] private float UIFadeTime;
        [SerializeField] private Color resizeColor;
        [SerializeField] private Color defaultColor;

        [SerializeField] private Transform holdingPlace;

        [SerializeField] private Camera cam;
        [SerializeField] private GameObject GrabButtonUI;
        [SerializeField] private GameObject DropButtonUI;
        [SerializeField] private GameObject ThrowButtonUI;

        private bool _grabbedThisFrame = false;
        private bool _isGrabbed = false;
        private bool isGrabClicked;
        private bool isDropClicked;
        private bool isThrowClicked;

        private bool isInRangeUI;
        private bool isInGrabUI;
        
        private Rigidbody _grabbedObj;

        void Start()
        {
            DOTween.Init();
            cam = Camera.main;

            GrabButtonUI.SetActive(false);
            DropButtonUI.SetActive(false);
            ThrowButtonUI.SetActive(false);
            isInRangeUI = false;
            isInGrabUI = false;
        }

        void Update()
        {
            _grabbedThisFrame = false;

            //to remove any forces from our object while holding it
            if (_isGrabbed)
            {
                StopItem();
                //grabbedObj.MovePosition(holdingPlace.position);
                //grabbedObj.MoveRotation(holdingPlace.rotation);
                if (Vector3.Distance(_grabbedObj.position, transform.position) > interactDistance)
                {
                    DropItem();
                }

                if (isThrowClicked)
                {
                    DropItem();
                    _grabbedObj.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                }
            }

            CheckIfInteractable();

            if (_isGrabbed && isDropClicked && !_grabbedThisFrame)
            {
                DropItem();
            }
            if (_isGrabbed)
            {
                if (!isInRangeUI)
                {
                    isInGrabUI = true;
                    UIShowUp(DropButtonUI, UIFadeTime);
                    UIShowUp(ThrowButtonUI, UIFadeTime);
                }
            }
            else
            {
                if (isInRangeUI)
                {
                    isInGrabUI = false;
                    UIShowUp(DropButtonUI, UIFadeTime, false);
                    UIShowUp(ThrowButtonUI, UIFadeTime, false);
                }
            }
        }

        private void FixedUpdate()
        {
            if (_isGrabbed)
            {
                _grabbedObj.MovePosition(Vector3.Lerp(_grabbedObj.transform.position, holdingPlace.position, Time.deltaTime * 25));
                //grabbedObj.transform.position = (Vector3.Lerp(grabbedObj.transform.position,holdingPlace.position,Time.deltaTime * 20));
                _grabbedObj.MoveRotation(Quaternion.Lerp(_grabbedObj.rotation,holdingPlace.rotation, Time.deltaTime * 50));
            }
        }

        private void CheckIfInteractable()
        {
            RaycastHit hitInfo;

            //Will check if we looking at some object in interactDistance
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, interactDistance))
            {
                //Every object with we can interact with need to have InteractiveObj component
                interactiveObject interactive = hitInfo.collider.gameObject.GetComponent<interactiveObject>();

                if (interactive != null && Vector3.SqrMagnitude(interactive.transform.position - transform.position) <= grabbableCheck)
                {
                    if (!_isGrabbed)
                    {
                        if (!isInRangeUI)
                        {
                            isInRangeUI = true;
                            UIShowUp(GrabButtonUI, UIFadeTime, true);
                        }
                        
                    }
                    else
                    {
                        if (isInRangeUI)
                        {
                            isInRangeUI = false;
                            UIShowUp(GrabButtonUI, UIFadeTime, false);
                        }
                        
                    }
                    //Show to the player that he is looking at an interactive object
                    if (cursor != null) EnlargeCursor();

                    //Check if we can grab it
                    if (interactive.IsGrabbable)
                    {
                        if (!_isGrabbed && isGrabClicked)
                        {
                            PickUpItem(hitInfo.collider.gameObject.GetComponent<Rigidbody>());
                            _grabbedThisFrame = true;
                        }
                    }

                    if (isGrabClicked) interactive.Interact();

                }
                else if (_isGrabbed)
                {
                    //If we can't see the grabbable object(and we see another interactive object) but we still hold it we need to drop it
                    //DropItem();
                }
                else
                {
                    if (isInRangeUI)
                    {
                        isInRangeUI = false;
                        UIShowUp(GrabButtonUI, UIFadeTime, false);
                    }
                    
                    CursorDefault();
                }

            }
            else if (_isGrabbed)
            {
                //If we can't see the grabbable object(and we see the non-interactive object) but we still hold it we need to drop it
                //DropItem();
            }
            else
            {
                //If we don't see any interactive objects 
                if (cursor != null) CursorDefault();
            }

        }

        private void EnlargeCursor()
        {
            cursor.localScale = new Vector3(resizeCursor + 1f, resizeCursor + 1f);
            cursor.GetComponent<Image>().color = resizeColor;
        }
    
        private void CursorDefault()
        {
            cursor.localScale = Vector3.one;
            cursor.GetComponent<Image>().color = defaultColor;
        }
    
        private void PickUpItem(Rigidbody rb)
        {
            //Check if the game object has Rigidbody
            if (rb != null)
            {
                _isGrabbed = true;
                _grabbedObj = rb;
                StopItem();
                _grabbedObj.useGravity = false; // to we can hold it
                //grabbedObj.mass = 100000f; // So it would win in colisions.
                //grabbedObj.transform.SetParent(transform); // to follow out position

            }
        }
        public void DropItem()
        {
            _isGrabbed = false;
            if (_grabbedObj != null)
            {

                StopItem();
                //Set all vars back to normal
                //grabbedObj.mass = 1;
                _grabbedObj.useGravity = true;
                //grabbedObj.transform.parent = null; ;
                //grabbedObj = null;
            }
        }
        private void StopItem()
        {
            if (_grabbedObj != null)
            {
                _grabbedObj.velocity = Vector3.zero;
                _grabbedObj.angularVelocity = Vector3.zero;
            }
        }

        public void ClickGrab()
        {
            isGrabClicked = true;
            StartCoroutine(ClickisGrabClickedEnd());
        }
        public void ClickDrop()
        {
            isDropClicked = true;
            StartCoroutine(ClickisDropClickedEnd());
        }
        public void ClickThrow()
        {
            isThrowClicked = true;
            StartCoroutine(ClickisThrowClickedEnd());
        }

        public void UIShowUp(GameObject UIElement, float fadeTime, bool FadeIn=true)
        {
            if (FadeIn)
            {
                UIElement.SetActive(true);
            }
            else
            {
                UIElement.SetActive(false);
            }
        }

        IEnumerator ClickisGrabClickedEnd()
        {
            yield return new WaitForEndOfFrame();
            isGrabClicked = false;
        }
        IEnumerator ClickisDropClickedEnd()
        {
            yield return new WaitForEndOfFrame();
            isDropClicked = false;
        }
        IEnumerator ClickisThrowClickedEnd()
        {
            yield return new WaitForEndOfFrame();
            isThrowClicked = false;
        }
}











