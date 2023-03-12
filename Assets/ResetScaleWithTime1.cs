using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ResetScaleWithTime1 : MonoBehaviour
{
    public float resetTime = 5.0f;
    public float compressedTime = 5.0f;
    public float speed = 1.0f;

    private float timer;
    private Vector3 originalScale;


    void Start()
    {
        originalScale = transform.localScale;
        timer = resetTime;
    }

    void Update()
    {
        Vector3 vectorMesh = transform.localScale;
        float growing = speed * Time.deltaTime;
        transform.localScale = new Vector3(vectorMesh.x + growing, vectorMesh.y + growing, vectorMesh.z + growing);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // Calculate the compressed scale
            float compressionAmount = 0.5f;
            Vector3 compressedScale = originalScale * compressionAmount;

            // Calculate the compressed time
            float compressedTime = 2.0f;

            // Reset the timer
            timer = compressedTime;

            // Set the new scale and time values
            originalScale = compressedScale;
           
            speed *= -2.0f; // reverse the direction of the scaling
        }
    }
}