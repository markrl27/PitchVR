using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;

public class PitchDetectDemo : MonoBehaviour
{
    [DllImport("AudioPluginDemo")]
    private static extern float PitchDetectorGetFreq(int index);

    [DllImport("AudioPluginDemo")]
    private static extern int PitchDetectorDebug(float[] data);

    float[] history = new float[1000];
    float[] debug = new float[65536];

    string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

    public Material mat;
    public string frequency = "detected frequency";
    public string note = "detected note";
    public AudioMixer mixer;
    public InfoText pitchText;


    string note1 = "F";
    string note2 = "E";
    string note3 = "F";
    string note4 = "G";

    bool note1Correct = false;
    bool note2Correct = false;
    bool note3Correct = false;
    bool note4Correct = false;
    public TMP_Text checkerText;

    AudioSource source;
    public bool isDetecting = false;
    public EchoScript echoScript;

    // Use this for initialization
    void Start()
    {
        checkerText.text = "";
        source = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {



        if (isDetecting && EchoScript.isInTrigger)
        {
            float freq = PitchDetectorGetFreq(0), deviation = 0.0f;
            frequency = freq.ToString() + " Hz";

            if (freq > 0.0f)
            {
                float noteval = 57.0f + 12.0f * Mathf.Log10(freq / 440.0f) / Mathf.Log10(2.0f);
                float f = Mathf.Floor(noteval + 0.5f);
                deviation = Mathf.Floor((noteval - f) * 100.0f);
                int noteIndex = (int)f % 12;
                int octave = (int)Mathf.Floor((noteval + 0.5f) / 12.0f);
                note = noteNames[noteIndex];


                if (note == note1)
                    note1Correct = true;
                if (note == note2)
                    note2Correct = true;
                if (note == note3)
                    note3Correct = true;
                if (note == note4)
                    note4Correct = true;

                note += " " + octave;

            }
            else
            {
                note = "unknown";
                pitchText.text = "";
            }

            if (pitchText != null)
                pitchText.text = "Detected frequency: " + frequency + "\nDetected note: " + note + " (deviation: " + deviation + " cents)";

        }

        if (source.isPlaying == false)
        {
            pitchText.text = "";
            note = "";

        }


        if (note1Correct == true && note2Correct == true && note3Correct == true && note4Correct == true)
        {
            checkerText.text = "Correct!";
            echoScript.CompleteArea();
        }


    }

    public void ResetPitch()
    {
        note1Correct = false;
        note2Correct = false;
        note3Correct = false;
        note4Correct = false;
        checkerText.text = "";
    }

    Vector3 Plot(float[] data, int num, float x0, float y0, float w, float h, Color col, float thr)
    {
        GL.Begin(GL.LINES);
        GL.Color(col);
        float xs = w / num, ys = h;
        float px = 0, py = 0;
        for (int n = 1; n < num; n++)
        {
            float nx = x0 + n * xs, ny = y0 + data[n] * ys;
            if (n > 1 && data[n] > thr && data[n - 1] > thr)
            {
                GL.Vertex3(px, py, 0);
                GL.Vertex3(nx, ny, 0);
            }
            px = nx;
            py = ny;
        }
        GL.End();
        return new Vector3(x0 + w, py, 0);
    }

    void OnRenderObject()
    {
        mat.SetPass(0);

        GL.Begin(GL.LINES);
        GL.Color(Color.green);
        GL.Vertex3(-5, 0, 0);
        GL.Vertex3(5, 0, 0);
        GL.End();

        for (int n = 1; n < history.Length; n++)
            history[n - 1] = history[n];
        history[history.Length - 1] = PitchDetectorGetFreq(0);
        transform.position = Plot(history, history.Length, -45.0f, 0.0f, 50.0f, 0.01f, Color.white, 0.1f);

        int num = PitchDetectorDebug(debug);
        Plot(debug, num, -5.0f, 1.0f, 10.0f, 0.0002f, Color.red, 0.1f);
    }

    void OnGUI()
    {
        float monitor;
        if (mixer != null && mixer.GetFloat("Monitor", out monitor))
        {
            GUILayout.Space(30);
            if (GUILayout.Button(monitor > 0.0f ? "Monitoring is ON" : "Monitoring is OFF"))
                monitor = (monitor > 0.0f) ? 0.0f : 0.5f;
            mixer.SetFloat("Monitor", monitor);
        }
    }
}
