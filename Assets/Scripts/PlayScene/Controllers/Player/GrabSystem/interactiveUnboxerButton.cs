using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class interactiveUnboxerButton : interactiveObject
{
    [SerializeField] private MakerTop UnboxerTop;
    [SerializeField] private Material buttonMaterial;
    [SerializeField] private Material PressedbuttonMaterial;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private UIManager _uiManager;
    public interactiveUnboxerButton()
    {
        IsGrabbable = false;
        IsBox = false;
    }
    public override void Interact()
    {
        if (UnboxerTop.ObjectsOnTop == 1)
        {
            UnboxerTop.EnterTheUnboxer();
            _collider.enabled = false;
            StartCoroutine(wait());
        }
        else
        {
            _uiManager.DisplayWorning("YOU CAN MAKE ONLY ONE OBJECT AT A TIME!");
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _uiManager = FindObjectOfType<UIManager>();
        UnboxerTop.OnShowing += OnShow;
        UnboxerTop.OnStopShowing += OnStopShow;
        _collider.enabled = false;
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnShow()
    {
        _collider.enabled = true;
    }
    private void OnStopShow()
    {
        _collider.enabled = false;
    }
    IEnumerator wait()
    {
        _collider.enabled = false;
        _meshRenderer.material = PressedbuttonMaterial;
        transform.DOLocalMoveY(-0.02f, .2f);
        yield return new WaitForSeconds(.7f);
        transform.DOLocalMoveY(0, .2f);
        _meshRenderer.material = buttonMaterial;
    }
}
