using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveUIElements : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform effectedTransform;
    //private GameObject SelectedIndicator;
    private RectTransform _rectTransform;
    public string ID;
    private String ResetPositionIDy = "PossitionIDy";
    private String ResetPositionIDx = "PossitionIDx";
    private Outline _outline;
    public void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas.GetComponent<UIManager>().Save += saveLayout;
        _canvas.GetComponent<UIManager>().Reset += resetLayout;
        if (!PlayerPrefs.HasKey(ResetPositionIDx+ID))
        {
            ID = gameObject.name;
            PlayerPrefs.SetFloat(ResetPositionIDx+ID, _rectTransform.anchoredPosition.x);
        }
        if (!PlayerPrefs.HasKey(ResetPositionIDy+ID))
        {
            ID = gameObject.name;
            PlayerPrefs.SetFloat(ResetPositionIDy+ID, _rectTransform.anchoredPosition.y);
        }

        _outline = gameObject.GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _outline.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _outline.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    private void saveLayout()
    {
        effectedTransform.anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    private void resetLayout()
    {
        _rectTransform.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(ResetPositionIDx+ID), PlayerPrefs.GetFloat(ResetPositionIDy+ID));
        print("gg");
    }
}
