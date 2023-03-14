using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneFeed : MonoBehaviour
{
    public bool useMicrophone = false;

    private AudioSource source;
    private string device;
    private bool prevUseMicrophone = false;
    private AudioClip prevClip = null;
    PitchDetectDemo pitchDetector;
    public EchoScript echoScript;
    public MicInput micInput;

    private void Start()
    {
        pitchDetector = FindObjectOfType<PitchDetectDemo>();
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

                source = GetComponent<AudioSource>();
                prevClip = source.clip;
                source.Stop();


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
                source.loop = false;

                //Microphone.End(device);
                source.clip = prevClip;
                source.Play();
            }
            Debug.Log(Microphone.IsRecording(device));

        }

    }

    void OnGUI()
    {
        if (GUILayout.Button(useMicrophone ? "Disable microphone" : "Enable microphone"))
            useMicrophone = !useMicrophone;
    }

    public void ToggleRecord()
    {
        useMicrophone = !useMicrophone;
        pitchDetector.isDetecting = !pitchDetector.isDetecting;
        micInput.InitMic();
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

    }

}
