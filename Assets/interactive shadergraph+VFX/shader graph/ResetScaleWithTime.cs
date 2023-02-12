using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScaleWithTime : MonoBehaviour
{ 
    public float resetTime = 5.0f;
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
            transform.localScale = originalScale;
            timer = resetTime;
        }
    }
}