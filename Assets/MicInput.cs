using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class MicInput : MonoBehaviour     // script adapted from: https://forum.unity.com/threads/check-current-microphone-input-volume.133501/ 
{
    #region SingleTon

    public static MicInput Inctance { set; get; }

    #endregion

    public static float MicLoudness;
    public static float MicLoudnessinDecibels;

    private string _device;

    public TMP_Text loudnessText;
    public GameObject scannerSphere;
    public Transform spawnLocation;
    public SoundWaveScript soundWaveScript;
    private bool activeSoundwave = false;
    float newSpeed, newTimer;
    float maxVolume = -200;

    //mic initialization
    public void InitMic()
    {
        if (_device == null)
        {
            _device = Microphone.devices[0];
        }
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
        _isInitialized = true;
    }

    public void StopMicrophone()
    {
        Microphone.End(_device);
        _isInitialized = false;
    }


    AudioClip _clipRecord;
    AudioClip _recordedClip;
    int _sampleWindow = 128;

    //get data from microphone into audioclip
    float MicrophoneLevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    //get data from microphone into audioclip
    public float MicrophoneLevelMaxDecibels()
    {

        float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));

        return db;
    }

    public float FloatLinearOfClip(AudioClip clip)
    {
        StopMicrophone();

        _recordedClip = clip;

        float levelMax = 0;
        float[] waveData = new float[_recordedClip.samples];

        _recordedClip.GetData(waveData, 0);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _recordedClip.samples; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    public float DecibelsOfClip(AudioClip clip)
    {
        StopMicrophone();

        _recordedClip = clip;

        float levelMax = 0;
        float[] waveData = new float[_recordedClip.samples];

        _recordedClip.GetData(waveData, 0);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _recordedClip.samples; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        float db = 20 * Mathf.Log10(Mathf.Abs(levelMax));

        return db;
    }



    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = MicrophoneLevelMax();
        MicLoudnessinDecibels = MicrophoneLevelMaxDecibels();

        loudnessText.text = MicLoudnessinDecibels + " Dbs";


        if(MicLoudnessinDecibels > -50 && maxVolume < MicLoudnessinDecibels && !activeSoundwave)
        {

            maxVolume = MicLoudnessinDecibels;

        }

        if(MicLoudnessinDecibels < -100 && maxVolume > -50 && !activeSoundwave && !MicrophoneFeed.useMicrophone)
        {
            GameObject spawnedSphere = Instantiate(scannerSphere, spawnLocation);
            //soundWaveScript = spawnedSphere.GetComponent<SoundWaveScript>();
            //newSpeed = math.remap(maxVolume, -50f, -10f, 2.0f, 0.2f);
            //soundWaveScript.SetSpeed(newSpeed);
            //newTimer = math.remap(MicLoudnessinDecibels, -60f, -10f, 2.0f, 6f);
            //soundWaveScript.SetTimer(newTimer);

            activeSoundwave = true;
            maxVolume = -200;
        }

    }

    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
        Inctance = this;
    }

    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");

        }
    }

    public void ResetSoundwaves()
    {
        activeSoundwave = false;
    }


}
