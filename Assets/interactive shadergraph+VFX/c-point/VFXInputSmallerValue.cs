
using UnityEngine;
using UnityEngine.VFX;


public class VFXInputSmallerValue : MonoBehaviour
{
   
    public VisualEffect vfxGraph; // Reference to the VFX graph
    public float originalFloatValue; // Original value of the float data
    public float newFloatValue; // Value of the float data

    void Start()
    {
        // Set the initial value of the new float data to the original value
        newFloatValue = originalFloatValue;

        // Set the initial value of the XScale float parameter in the VFX graph to the original value
        vfxGraph.SetFloat("Rate", originalFloatValue);
    }

    void Update()
    {
        // Check if the "0" key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // Reset the value of the new float data to the original value
            newFloatValue += Time.deltaTime;
            newFloatValue = originalFloatValue;
        }

        // Modify the value of the new float data in real-time
        newFloatValue += Time.deltaTime;

        // Update the value of the XScale float parameter in the VFX graph
        vfxGraph.SetFloat("Rate", newFloatValue);
    }
}
