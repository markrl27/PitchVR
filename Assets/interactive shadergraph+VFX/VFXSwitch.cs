using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSwitch : MonoBehaviour
{
    public GameObject vfxObject1;
    public GameObject vfxObject2;
    public GameObject shadergraphObject1;
    public GameObject vfxObject3;
    public GameObject vfxObject4;
    public GameObject vfxObject5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            vfxObject1.SetActive(true);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(false);
            vfxObject3.SetActive(false);
            vfxObject4.SetActive(false);
            vfxObject5.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(true);
            shadergraphObject1.SetActive(false);
            vfxObject3.SetActive(false);
            vfxObject4.SetActive(false);
            vfxObject5.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(true);
            vfxObject3.SetActive(false);
            vfxObject4.SetActive(false);
            vfxObject5.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(false);
            vfxObject3.SetActive(true);
            vfxObject4.SetActive(false);
            vfxObject5.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(false);
            vfxObject3.SetActive(false);
            vfxObject4.SetActive(true);
            vfxObject5.SetActive(false);


        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            vfxObject1.SetActive(true);
            vfxObject2.SetActive(true);
            shadergraphObject1.SetActive(false);
            vfxObject3.SetActive(false);
            vfxObject4.SetActive(true);
            vfxObject5.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(true);
            shadergraphObject1.SetActive(true);
            vfxObject3.SetActive(false);
            vfxObject4.SetActive(true);
            vfxObject5.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(true);
            vfxObject3.SetActive(true);
            vfxObject4.SetActive(true);
            vfxObject5.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            vfxObject1.SetActive(true);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(true);
            vfxObject3.SetActive(true);
            vfxObject4.SetActive(false);
            vfxObject5.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            vfxObject1.SetActive(false);
            vfxObject2.SetActive(false);
            shadergraphObject1.SetActive(true);
            vfxObject3.SetActive(false);
            vfxObject5.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            vfxObject1.SetActive(true);
            vfxObject2.SetActive(true);
            shadergraphObject1.SetActive(true);
            vfxObject3.SetActive(true);
            vfxObject5.SetActive(true);
        }
    }
}
   

