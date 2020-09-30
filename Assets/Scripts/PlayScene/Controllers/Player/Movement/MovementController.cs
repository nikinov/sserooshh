using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MovementController : MonoBehaviour
{
    public FloatingJoystick MoveJoystick;
    public FixedButton JumpButton;
    public FixedTouchField touchField;

    private RigidbodyFirstPersonController fps;
    // Start is called before the first frame update
    void Start()
    {
        fps = GetComponent<RigidbodyFirstPersonController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        fps.RunAxis = MoveJoystick.Direction;
        fps.JumpAxis = JumpButton.Pressed;
        fps.mouseLook.LookAxis = touchField.TouchDist;
    }
}
