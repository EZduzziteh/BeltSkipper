using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float timer;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = Time.time + 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= Time.time)
        {
            rb.drag = 0.0f;
            rb.angularDrag = 0.0f;
            timer = Mathf.Infinity;
        }   
    }
}
