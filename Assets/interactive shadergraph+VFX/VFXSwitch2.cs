using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSwitch2 : MonoBehaviour
{
    public GameObject vfxObject1;
    public GameObject vfxObject2;
    public GameObject shadergraphObject1;
    public GameObject vfxObject3;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            vfxObject1.SetActive(true);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(false);
            vfxObject3.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(true);
            shadergraphObject1.SetActive(false);
            vfxObject3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(true);
            vfxObject3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(false);
            vfxObject3.SetActive(true);
        }

    }
}

