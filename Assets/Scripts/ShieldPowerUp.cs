using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public Rocket playerreference;

    private void Start()
    {
        playerreference
            = FindObjectOfType<Rocket>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            other.GetComponent<HealthComponent>().StartShield();
            other.GetComponent<Rocket>().PlayPositive();
            transform.position = new Vector3(transform.position.x - 1000 + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5), transform.position.z + Random.Range(-5, 5));
        }
    }

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
        transform.position = new Vector3(transform.position.x - 1200, transform.position.y, transform.position.z);
    }
}
