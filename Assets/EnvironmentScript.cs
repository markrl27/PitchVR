using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Unity.Mathematics;

public class EnvironmentScript : MonoBehaviour
{

    private VisualEffect visualEffect;
    float loudness, newXscale;


    ParticleSystem system;

    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<ParticleSystem>();

        if(GetComponent<VisualEffect>() != null)
        {
            visualEffect = GetComponent<VisualEffect>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VisualEffect>() != null)
        {
            loudness = MicInput.MicLoudnessinDecibels;
            newXscale = math.remap( -200.0f, -30.0f, 0.1f, 2.0f, loudness);
            visualEffect.SetFloat("ScaleX", newXscale);
        }




    }


    public void PlayParticles()
    {
        system.Play();
    }

    public void StopParticles()
    {
        system.Stop();
    }

}
