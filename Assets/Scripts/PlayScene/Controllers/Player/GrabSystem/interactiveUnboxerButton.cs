using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactiveUnboxerButton : interactiveObject
{
    [SerializeField] private MakerTop UnboxerTop;
    public interactiveUnboxerButton()
    {
        IsGrabbable = false;
        IsBox = false;
    }
    public override void Interact()
    {
        UnboxerTop.EnterTheUnboxer();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
