using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InputCapture : MonoBehaviour
{

    public MicrophoneFeed microphoneFeed;
    public PostProcessing ppScript;
    public WalkInPlaceLocomotion walkInPlace;
    public XRInteractorLineVisual lineVisual;

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
        //microphoneFeed = FindObjectOfType<MicrophoneFeed>();
        lineVisual.enabled = false;
    }

    void onRightGripPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("Right Grip Pressed");

        StartCoroutine(microphoneFeed.ToggleRecord());
        ppScript.TogglePostProcessing();

    }
    void onLeftGripPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("Left Grip Pressed");
    }



    public void ToggleWalkInPlace()
    {
        StartCoroutine(ToggleCoroutine());
    }


    public IEnumerator ToggleCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        if (walkInPlace.enabled == true)
        {
            walkInPlace.enabled = false;
            lineVisual.enabled = true;
        }
        else
        {
            walkInPlace.enabled = true;
            lineVisual.enabled = false;
        }


        Debug.Log(walkInPlace.enabled);

    }


}

