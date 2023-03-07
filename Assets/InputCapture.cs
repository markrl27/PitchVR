using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCapture : MonoBehaviour
{

    [SerializeField] InputActionReference leftControllerGrip;

    private void Awake()
    {
        leftControllerGrip.action.performed += onLeftGripPressed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void onLeftGripPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("Left Grip Pressed");
    }

}
