using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PostProcessing : MonoBehaviour
{

    Volume volume;
    ColorAdjustments colorAdjustments;
    public Image recordingImage;
    public Image recordingImage1;
    
    bool recordingPPOn = false;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        recordingImage.enabled = false;
        recordingImage1.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePostProcessing()
    {
        if (!recordingPPOn)
        {
            colorAdjustments.saturation.value = -70;
            recordingImage.enabled = true;
            recordingImage1.enabled = true;
        }
        else
        {
            colorAdjustments.saturation.value = 30;
            recordingImage.enabled = false;
            recordingImage1.enabled = false;
        }
        recordingPPOn = !recordingPPOn;
    }


}
