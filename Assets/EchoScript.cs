using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoScript : MonoBehaviour
{

    public AudioSource source;
    private float timer = 5f;
    public bool timerActive = true;
    PitchDetectDemo pitchDetector;
    MicrophoneFeed microphoneFeed;
    public GameObject affectedEnvironment;
    public Material newMaterial;
    AudioClip clip;
    public static bool isInTrigger = false;



    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        pitchDetector = FindObjectOfType<PitchDetectDemo>();
        microphoneFeed = FindObjectOfType<MicrophoneFeed>();
        clip = source.clip;
        
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
            //pitchDetector.inEchoZone = true;
            isInTrigger = true;
            microphoneFeed.echoScript = this;
            pitchDetector.echoScript = this;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //pitchDetector.inEchoZone = false;
            isInTrigger = false;
            microphoneFeed.echoScript = null;
        }


    }

    public void PauseClip()
    {
        timerActive = false;
        source.Stop();
        source.clip = null;
    }

    public void PlayClip()
    {
        timer = 1;
        timerActive = true;
        source.clip = clip;
    }



    public void CompleteArea()
    {
        foreach (Transform child in affectedEnvironment.transform)
        {
            child.GetComponent<MeshRenderer>().material = newMaterial;
        }
    }
}
