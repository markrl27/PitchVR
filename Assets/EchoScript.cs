using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoScript : MonoBehaviour
{

    public AudioSource source;
    private float timer = 5f;
    public bool timerActive = true;
    PitchDetectDemo pitchDetector;



    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        pitchDetector = FindObjectOfType<PitchDetectDemo>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
        }


        if(timer <= 0)
        {
            source.Play();
            timer = 5f;
            timerActive = false;

        }

        if(source.isPlaying == false && !timerActive)
        {
            timerActive = true;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pitchDetector.isDetecting = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pitchDetector.isDetecting = false;
        }


    }
}
