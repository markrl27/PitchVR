using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneFeed : MonoBehaviour
{
    public static bool useMicrophone = false;

    private AudioSource source;
    private string device;
    private bool prevUseMicrophone = false;
    //private AudioClip prevClip = null;
    PitchDetectDemo pitchDetector;
    public EchoScript echoScript;
    public MicInput micInput;


    public static float clipLoudness;
    private float[] clipSampleData = new float[1024];
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;
    private float currentUpdateTime = 0f;

    private void Start()
    {
        pitchDetector = FindObjectOfType<PitchDetectDemo>();
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (useMicrophone != prevUseMicrophone)
        {
            prevUseMicrophone = useMicrophone;
            if (useMicrophone)
            {
                

                foreach (string m in Microphone.devices)
                {
                    device = m;
                    Debug.Log(m);
                    break;
                }

                
                //prevClip = source.clip;
                //source.Stop();


                source.clip = Microphone.Start(device, true, 1, AudioSettings.outputSampleRate);
                Debug.Log(Microphone.IsRecording(device));
                source.loop = true;
                source.volume = 5;
                source.Play();

                int dspBufferSize, dspNumBuffers;
                AudioSettings.GetDSPBufferSize(out dspBufferSize, out dspNumBuffers);

                source.timeSamples = (Microphone.GetPosition(device) + AudioSettings.outputSampleRate - 3 * dspBufferSize * dspNumBuffers) % AudioSettings.outputSampleRate;
            }
            else
            {
                source.Stop();
                source.loop = false;

                //Microphone.End(device);
                //source.clip = prevClip;
                //source.Play();
            }
            Debug.Log(Microphone.IsRecording(device));

        }


        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;

            if(useMicrophone)
            source.clip.GetData(clipSampleData, source.timeSamples); 
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; 
        }
        //Debug.Log(clipLoudness.ToString());

    }

    void OnGUI()
    {
        if (GUILayout.Button(useMicrophone ? "Disable microphone" : "Enable microphone"))
            useMicrophone = !useMicrophone;
    }

    public IEnumerator ToggleRecord()
    {
        if (useMicrophone)
            micInput.InitMic();

        useMicrophone = !useMicrophone;
        
        if (EchoScript.isInTrigger)
        {
            if (useMicrophone)
            {
                echoScript.PauseClip();
            }
            else
            {
                echoScript.PlayClip();
            }

        }
        pitchDetector.note = "";
        yield return new WaitForSeconds(1f);
        pitchDetector.isDetecting = !pitchDetector.isDetecting;

    }

}
