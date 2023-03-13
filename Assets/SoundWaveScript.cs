using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //materialOriginal = other.GetComponent<MeshRenderer>().material;
            other.GetComponent<MeshRenderer>().material = materialNew;
            environmentScript = other.GetComponent<EnvironmentScript>();
            environmentScript.PlayParticles();
        }
        
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnvironmentScript>() != null)
        {
            other.GetComponent<MeshRenderer>().material = materialOriginal;
            environmentScript = other.GetComponent<EnvironmentScript>();
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