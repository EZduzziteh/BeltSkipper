using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityComponent : MonoBehaviour
{
    [SerializeField]
    bool GravityEnabled=true;
    [SerializeField]
    bool randomtorque = true;
    [SerializeField]
    float torquemax = 10000.0f;
    [SerializeField]
    private float InitialSpeed;

    private List<GravityComponent> ActorsInGravityWell = new List<GravityComponent>();

    private Rigidbody rb;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();

        rb.AddRelativeForce(Vector3.forward * InitialSpeed);
        if (randomtorque)
        {
            rb.AddRelativeTorque(Vector3.right * Random.Range(-torquemax, torquemax));
            rb.AddRelativeTorque(Vector3.forward * Random.Range(-torquemax, torquemax));
        }
    }

 
    void FixedUpdate()
    {
     
        if (GravityEnabled)
        {
            foreach (GravityComponent actor in ActorsInGravityWell)
            {
                if (actor)
                {
                    //Calculate forces to apply and apply them
                    ApplyGravity(CalculateGravityForce(actor), CalculateGravityDirection(actor));
                }
                
            }
        }
    }

    //Gets force magnitude between this object and other object
    float CalculateGravityForce(GravityComponent otherActor) {

        //check to make sure we never divide by 0
        if (Vector3.Distance(transform.position, otherActor.transform.position) == 0)
        {
            return 0.0f;
        }

        //F = Gm1m2/r2  //g=6.67408*10^-11
        float F = ((6.67408e-11f)*(rb.mass)*(otherActor.GetComponent<Rigidbody>().mass))
                  / (Vector3.Distance(transform.position,otherActor.transform.position));
        //Debug.Log(F*1000000);
        return F * 1000000;
    }

    //Gest unit vector towards desired direction
    Vector3 CalculateGravityDirection(GravityComponent otherActor)
    {
        return Vector3.Normalize(otherActor.transform.position - transform.position);
    }

    //adds gravity in worldspace in a specific direction with a specific magnitude
    void ApplyGravity(float magnitude, Vector3 direction)
    {
        rb.AddForce(direction * magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if its a gravity componenet then add it to the gravity well
        if (other.GetComponent<GravityComponent>())
        {

                if (ActorsInGravityWell.Contains(other.GetComponent<GravityComponent>()))
                {
                //dont add if already in the list
                }
            

                if (other.gameObject.GetComponent<Projectile>())
                {
                    //dont add if projectile
                }
                else
                {
                    ActorsInGravityWell.Add(other.GetComponent<GravityComponent>());
                }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if leaves sphere of influence, remove from gravity well
        if (other.GetComponent<GravityComponent>())
        {
            if (other.gameObject.GetComponent<Projectile>())
            {
                //dont remove if projectile
            }
            else
            {
                ActorsInGravityWell.Remove(other.GetComponent<GravityComponent>());
            }
        }
    }

    public void RemoveGravityActor(GravityComponent actor)
    {
        List<GravityComponent> templist = new List<GravityComponent>();
        foreach(GravityComponent gravActor in ActorsInGravityWell)
        {
            if (gravActor != actor)
            {
                templist.Add(gravActor);
            }
            
        }
        ActorsInGravityWell.Clear();
        foreach(GravityComponent gravActor in templist)
        {
            if(!ActorsInGravityWell.Contains(gravActor))
            ActorsInGravityWell.Add(gravActor);
        }


    }
}
