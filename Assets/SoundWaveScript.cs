using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SoundWaveScript : MonoBehaviour
{ 
    public float resetTime = 5.0f;
    public float speed = 1.0f;

    private float timer;
    private Vector3 originalScale;


    public Material materialOriginal;
    public Material materialNew;
    private EnvironmentScript environmentScript;
    public MicInput micInput;

    void Start()
    {
        originalScale = transform.localScale;
        timer = resetTime;

        micInput = FindObjectOfType<MicInput>();
    }

    void Update()
    {
        Vector3 vectorMesh = transform.localScale;
        float growing = speed * Time.deltaTime;
        transform.localScale = new Vector3(vectorMesh.x + growing, vectorMesh.y + growing, vectorMesh.z + growing);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            transform.localScale = originalScale;
            timer = resetTime;

            micInput.ResetSoundwaves();

            Destroy(gameObject, 0.1f);
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnvironmentScript>() != null)
        {
            environmentScript = other.GetComponent<EnvironmentScript>();
            if (environmentScript.lvlComplete == false)
            {
                //materialOriginal = other.GetComponent<MeshRenderer>().material;
                other.GetComponent<MeshRenderer>().material = materialNew;

                
            }
            environmentScript.PlayParticles();

        }

        if(other.GetComponent<VisualEffect>() != null)
        {
            other.GetComponent<VisualEffect>().SetFloat("Rate", 5000);
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

        if (other.GetComponent<VisualEffect>() != null)
        {
            other.GetComponent<VisualEffect>().SetFloat("Rate", 0);
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