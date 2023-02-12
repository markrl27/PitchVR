using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scanner_Controller : MonoBehaviour
{
    private Vector3 originalScale;

    [Header("speed")]
    public float speed;

    [Header("destroy time")]
    public float delay_destroy_time;

    [Header("resetTime")]
    public float resetTime;



    // Start is called before the first frame update
    void Start()
    {

        originalScale = transform.localScale;

        destory_object();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale == originalScale)
        {
            Debug.Log("Update function triggered");
        }

            Vector3 vectorMesh = this.transform.localScale;
        float growing = this.speed * Time.deltaTime;
        this.transform.localScale = new Vector3(vectorMesh.x + growing, vectorMesh.y + growing, vectorMesh.z + growing);
      
    }

    private void destory_object()
    {
        Destroy(this.gameObject, delay_destroy_time);
    }

    void OnDestroy()
    {

        Invoke("InstantiateObject", resetTime); }

    void InstantiateObject()
    { 

        GameObject newObject = Instantiate(gameObject, transform.position, transform.rotation);
        newObject.transform.localScale = originalScale;
        newObject = this.gameObject;
    }
}


