using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public GameObject Target;
    public GameObject CamAnchor;
    public float lag;
    public float StopDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CamAnchor)
        {
            if (Vector3.Distance(transform.position, CamAnchor.transform.position) <= StopDistance)
            {

            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, CamAnchor.transform.position, lag * Time.deltaTime * Vector3.Distance(transform.position, CamAnchor.transform.position));

            }
        }
    }
    private void LateUpdate()
    {
        if (CamAnchor)
        {
            transform.rotation = CamAnchor.transform.rotation;
        }
       // transform.LookAt(Target.transform);
    }
}
