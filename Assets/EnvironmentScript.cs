using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Unity.Mathematics;

public class EnvironmentScript : MonoBehaviour
{

    private VisualEffect visualEffect;
    float loudness, newXscale;
    public bool lvlComplete = false;
    float minScale, maxScale;


    ParticleSystem system;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<ParticleSystem>() != null)
        {
            system = GetComponent<ParticleSystem>();
        }
        

        if(GetComponent<VisualEffect>() != null)
        {
            visualEffect = GetComponent<VisualEffect>();
        }

        minScale = 0.005f;
        maxScale = 2.5f;
        newXscale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (visualEffect != null)
        {
            if (newXscale >= minScale && newXscale <= maxScale)
            {
                float diff;
                if (MicInput.MicLoudnessinDecibels >= -160 && MicInput.MicLoudnessinDecibels <= -10)
                {
                    diff = math.remap(-160.0f, -10.0f, -0.2f, 0.4f, MicInput.MicLoudnessinDecibels);
                }
                else
                {
                    diff = 0;
                }
                
                newXscale += diff * Time.deltaTime;
            }
            else
                newXscale = minScale;

            if(newXscale < 0.01f)
            {
                newXscale = minScale;
            }

            visualEffect.SetFloat("ScaleX", newXscale);
        }




    }


    public void PlayParticles()
    {
        if (system != null)
        {
            system.Play();
        }

    }

    public void StopParticles()
    {
        if (system != null)
        {
            system.Stop();
        }

    }

}
