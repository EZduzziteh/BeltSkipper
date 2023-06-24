using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float CurrentHealth = 100.0f;
    [SerializeField]
    private float MaxHealth = 100.0f;
    [SerializeField]
    float CollisionVelocityThreshold=10.0f;
    [SerializeField]
    float ShieldLength=10.0f;
    private float ShieldTimer=0.0f;

    public GameObject LoseScreen;

    public GameObject ShieldText;


    public GameObject DestroyParticle;
    public GameObject ShieldParticle;
    bool HasShield;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (HasShield)
        {
            if (ShieldTimer <= Time.time)
            {
                StopShield();
            }
        }
    }
    public void StartShield()
    {
        gameObject.layer = 8;
        HasShield = true;
        ShieldParticle.SetActive(true);
        ShieldText.SetActive(true);
        ShieldTimer = Time.time + ShieldLength;
      
    }
    public void StopShield()
    {

            gameObject.layer = 9;
            HasShield = false;
            ShieldParticle.SetActive(false);
            ShieldText.SetActive(false);


    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Took" + damage + "damage");
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            //check if gravity actor to update other gravity actors
            if (GetComponent<GravityComponent>())
            {
                GetComponent<GravityComponent>().RemoveGravityActor(GetComponent<GravityComponent>());

                
            }//if its an asteroid trigger resource drops
            if (GetComponent<Asteroid>())
            {
                GetComponent<Asteroid>().DropResource();
            }

            //finally, destroy
            if (DestroyParticle)
            {
                DestroyParticle.gameObject.SetActive(true);
                DestroyParticle.transform.parent = null;
            }

            LoseScreen.SetActive(true);
            FindObjectOfType<ADManager>().StartAdTimer(2.0f);
            Cursor.visible = true;
            Destroy(this.gameObject);
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
       
            
                TakeDamage(Vector3.Magnitude(collision.relativeVelocity) * 100.0f);
            
        

        
    }


}
