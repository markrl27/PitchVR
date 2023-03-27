using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBuff : MonoBehaviour
{

    public ComputeShader computeShader;
    public int bufferSize;

    void Start()
    {
        // Create a new ComputeBuffer with the desired size
        ComputeBuffer buffer = new ComputeBuffer(bufferSize, sizeof(float));

        // Set the buffer at index 4 in the compute shader
        computeShader.SetBuffer(0, 4, buffer);
    }
}