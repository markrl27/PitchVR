using UnityEngine;
using System.Collections;



public class Reset : MonoBehaviour
{
    public float resetTime = 5.0f;

    void OnDestroy()
    {
        Invoke("InstantiateObject", resetTime);
    }

    void InstantiateObject()
    {
        Instantiate(gameObject, transform.position, transform.rotation);
    }
}