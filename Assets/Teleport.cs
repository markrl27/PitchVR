using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public Transform teleportLocation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other);
            var charactercontroller = other.GetComponent<CharacterController>();
            charactercontroller.enabled = false;
                
            other.transform.position = teleportLocation.position;
            charactercontroller.enabled = true;
        }
    }

}
