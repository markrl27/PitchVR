using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScript : MonoBehaviour
{


    ParticleSystem system;

    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayParticles()
    {
        system.Play();
    }

    public void StopParticles()
    {
        system.Stop();
    }

}
