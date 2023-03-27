using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EchoScript : MonoBehaviour
{

    public AudioSource source;
    private float timer = 3f;
    public bool timerActive = true;
    PitchDetectDemo pitchDetector;
    MicrophoneFeed microphoneFeed;
    public GameObject affectedEnvironment;
    public Material newMaterial;
    AudioClip clip;
    public static bool isInTrigger = false;
    PostProcessing postProcessing;
    EnvironmentScript environmentScript;

    ParticleSystem system;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        pitchDetector = FindObjectOfType<PitchDetectDemo>();
        microphoneFeed = FindObjectOfType<MicrophoneFeed>();
        postProcessing = FindObjectOfType<PostProcessing>();
        clip = source.clip;

        system = GetComponent<ParticleSystem>();


        foreach (Transform child in affectedEnvironment.transform)
        {
            //child.GetComponent<MeshRenderer>().material = newMaterial;
            //environmentScript = child.GetComponent<EnvironmentScript>();
            //environmentScript.lvlComplete = true;

            if (child.GetComponent<VisualEffect>() != null)
            {
                child.GetComponent<VisualEffect>().Stop();
            }

        }

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
            if (system != null)
                system.Play();
            timer = 3f;
            timerActive = false;

        }

        if(source.isPlaying == false && !timerActive)
        {
            timerActive = true;
            if(system != null)
                system.Stop();
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pitchDetector.ResetPitch();
            isInTrigger = true;
            microphoneFeed.echoScript = this;
            pitchDetector.echoScript = this;
            if (MicrophoneFeed.useMicrophone)
            {
                PauseClip();
            }


            if (gameObject.CompareTag("Echo1"))
            {
                pitchDetector.SetEcho1();
            }
            if (gameObject.CompareTag("Echo2"))
            {
                pitchDetector.SetEcho2();
            }
            if (gameObject.CompareTag("Echo3"))
            {
                pitchDetector.SetEcho3();
            }
            if (gameObject.CompareTag("TestEcho"))
            {
                pitchDetector.SetTestEcho();
            }

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExitTrigger();
        }


    }

    public void PauseClip()
    {
        timerActive = false;
        source.Stop();
        source.clip = null;
        pitchDetector.note = "";
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
            //child.GetComponent<MeshRenderer>().material = newMaterial;
            //environmentScript = child.GetComponent<EnvironmentScript>();
            //environmentScript.lvlComplete = true;

            if(child.GetComponent<VisualEffect>() != null)
            {
                child.GetComponent<VisualEffect>().Play();
            }

        }
        PauseClip();
        ExitTrigger();

        Destroy(this);
        Destroy(gameObject);
    }


    public void ExitTrigger()
    {
        //pitchDetector.inEchoZone = false;
        isInTrigger = false;
        microphoneFeed.echoScript = null;
        if (MicrophoneFeed.useMicrophone)
        {
            StartCoroutine(microphoneFeed.ToggleRecord());
            postProcessing.TogglePostProcessing();
            PlayClip();
        }

        pitchDetector.SetNoEcho();
        pitchDetector.ResetPitch();
    }

}
