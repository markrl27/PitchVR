using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Unity.Mathematics;

public class SoundWaveScript : MonoBehaviour
{ 
    public float resetTime = 7.0f;
    public float speed = 2f;

    private float timer;
    private Vector3 originalScale;


    public Material materialOriginal;
    public Material materialNew;
    private EnvironmentScript environmentScript;
     MicInput micInput;
    float scale, minScale, maxScale;

    public ParticleSystem pSystem;

    void Start()
    {
        originalScale = transform.localScale;
        timer = resetTime;

        micInput = FindObjectOfType<MicInput>();

        minScale = 0f;
        maxScale = 120;
        scale = 10;
    }

    void Update()
    {
        //Vector3 vectorMesh = transform.localScale;
        //float growing = speed * Time.deltaTime;
        //transform.localScale = new Vector3(vectorMesh.x + growing, vectorMesh.y + growing, vectorMesh.z + growing);

        //timer -= Time.deltaTime;

        if (scale > 2 && pSystem.isPlaying == false)
        {
            pSystem.Play();
        }
        
        if(scale <= 2 && pSystem.isPlaying == true)
        {
            pSystem.Stop();
        }


        if (timer <= 0)
        {
            transform.localScale = originalScale;
            timer = resetTime;

            micInput.ResetSoundwaves();

            Destroy(gameObject, 0.1f);
        }

        if(scale >= minScale && scale <= maxScale)
        {
            float diff;
            if (MicInput.MicLoudnessinDecibels >= -160 && MicInput.MicLoudnessinDecibels <= -10)
            {
                diff = math.remap(-160f, -10f, -20f, 50f, MicInput.MicLoudnessinDecibels);
            }
            else
            {
                diff = 0;
                
            }

                scale += diff * Time.deltaTime;
        }
        else
        {
            scale = minScale;
        }


        if(scale < (minScale + 0.1))
        {
            scale = minScale;
        }

        transform.localScale = new Vector3(scale, scale, scale);

    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnvironmentScript>() != null)
        {
            environmentScript = other.GetComponent<EnvironmentScript>();
            if (environmentScript.lvlComplete == false)
            {
                other.GetComponent<MeshRenderer>().material = materialNew;
            }
            environmentScript.PlayParticles();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnvironmentScript>() != null)
        {
            environmentScript = other.GetComponent<EnvironmentScript>();
            if(environmentScript.lvlComplete == false)
            {
                other.GetComponent<MeshRenderer>().material = materialOriginal;
            }
            environmentScript.StopParticles();

        }
    }



    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void SetTimer(float _timer)
    {
        resetTime = _timer;
    }
}