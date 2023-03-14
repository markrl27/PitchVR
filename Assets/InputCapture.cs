using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCapture : MonoBehaviour
{

    MicrophoneFeed microphoneFeed;
    //bool isRecording = false;




    [Header("Select Action")]
    [SerializeField] InputActionReference rightControllerGrip, leftControllerGrip;
    void Awake()
    {
        rightControllerGrip.action.performed += onRightGripPressed;
        leftControllerGrip.action.performed += onLeftGripPressed;
    }


    private void Start()
    {
        microphoneFeed = FindObjectOfType<MicrophoneFeed>();
    }

    void onRightGripPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("Right Grip Pressed");

        microphoneFeed.ToggleRecord();

    }
    void onLeftGripPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("Left Grip Pressed");
    }

}

