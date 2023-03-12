
using UnityEngine;
using UnityEngine.VFX;


public class VFXcontroller : MonoBehaviour
{
    public VisualEffect vfxGraph; // Reference to the VFX graph

    // Name of the float data to control
   

    public float newfloatvalue;

    void Update()
    {
        // Get the value of the float data
        float xcale = vfxGraph.GetFloat("Rate");

        //print
        Debug.Log("Rate is" + xcale);


        if (Input.GetKeyDown(KeyCode.Alpha9))
        {

            newfloatvalue += Time.deltaTime;

            vfxGraph.SetFloat("Rate", newfloatvalue);
        }

    }
}