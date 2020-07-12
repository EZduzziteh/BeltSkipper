using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private Rocket playerreference;
    // Start is called before the first frame update
    void Start()
    {
        playerreference = FindObjectOfType<Rocket>();
        float randomnum=Random.Range(15.0f, 40.0f);
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 1);
        transform.position = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(-2, 2), transform.position.z + Random.Range(-2, 2));
        transform.localScale = new Vector3(randomnum, randomnum, randomnum);
        GetComponent<Rigidbody>().AddRelativeTorque(transform.forward * Random.Range(-1000000000, 1000000000));
        GetComponent<Rigidbody>().AddRelativeTorque(transform.right * Random.Range(-1000000000, 1000000000)); 
       
    }

    // Update is called once per frame
    void Update()
    {
        //when player passes the planet by 50 units, relocate
        if (playerreference)
        {
            if (playerreference.transform.position.x <= transform.position.x - 10.0f)
            {
                Relocate();
            }
        }
    }

    void Relocate()
    {
        //Debug.Log("relocating");
        //set position farther forward
        transform.position = new Vector3(transform.position.x-1200, transform.position.y, transform.position.z );
    }
}
