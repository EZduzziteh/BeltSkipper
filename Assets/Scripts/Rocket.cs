using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{ 
    //Variables to be set
    [SerializeField]
    private bool RandomInitialTorque = true;
    [SerializeField]
    private float LaunchForce=1000f;
    [SerializeField]
    private float LaunchTorque = 10.0f;
    [SerializeField]
    public float TorqueSpeed=100.0f;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float Acceleration;
    [SerializeField]
    private float MaxTimeBetweenMalfunction = 30.0f;
    [SerializeField]
    private float MinTimeBetweenMalfunction = 10.0f;
    [SerializeField]
    private float BoostForce = 10000f;

    //nonset variables
    private float DistanceTravelled = 0;
    private float Velocity = 0;
    private Vector3 StartPoint;
    private float MalfunctionTimer;
    public bool HasBoost=true;
    private bool hasstarted=false;



    //References
    public ParticleSystem BoostParts;
    public ParticleSystem Fire;
    public Text VelocityText;
    public Text DistanceText;
    public Text HighestVelocityText;
    public Text LongestDistanceText;
    public GameObject MalfunctionText;
    public GameObject BoostText;
    private Rigidbody rb;
    public Transform ProjectileSpawnPoint;
    private AudioSource aud;
    public AudioClip Negative;
    public AudioClip boostclip;
    public AudioClip Positive;
    public AudioClip Malfunctionclip;
    public AudioClip Malfunctionclip1;
    public AudioClip AlarmClip;

    private int LongestDistance;
    private int HighestVelocity;
    
    // Start is called before the first frame update
    void Start()

    {
      
        rb = GetComponent<Rigidbody>();
        aud = GetComponent<AudioSource>();
        LongestDistance = PlayerPrefs.GetInt("LongestDistance");
        LongestDistanceText.text = "Longest Distance: " + LongestDistance;
        HighestVelocity = PlayerPrefs.GetInt("HighestVelocity");
        HighestVelocityText.text = "highest Velocity: " + HighestVelocity;
        StartPoint = new Vector3(transform.position.x,transform.position.y,transform.position.z);
  
        MalfunctionTimer = Time.time + Random.Range(MinTimeBetweenMalfunction, MaxTimeBetweenMalfunction);
       
        //set initial thrust and torque
        Thrust(LaunchForce);
        Torque(LaunchTorque);
        Cursor.visible = false;

    }



    private void FixedUpdate()
    {
        if (rb.angularVelocity.magnitude <= 1.0f)
        {
            MalfunctionText.SetActive(false);
        }

            if (hasstarted)
        {
            if (rb.velocity.magnitude <= 2.0f & !HasBoost)
            {
                Debug.LogError("speed too low and no boost");
                GetComponent<HealthComponent>().TakeDamage(500);
            }
            else if (rb.velocity.magnitude <= 10.0f & HasBoost)
            {
                Boost();
            }
        }
        else
        {
            hasstarted = true;
        }
       
        

        //trigger malfunction if time
        if (Time.time > MalfunctionTimer)
        {
            Malfunction();
            //reset malfunction timer
            MalfunctionTimer = Time.time + Random.Range(MinTimeBetweenMalfunction, MaxTimeBetweenMalfunction);

        }
       

        //player inputs
            UpThrust((Speed *Velocity) *Input.GetAxis("Vertical"));
            Strafe((Speed * Velocity) *Input.GetAxis("Strafe"));
            Thrust(Acceleration);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (HasBoost)
            {
                Boost();
            
            }
            else
            {
                aud.clip = Negative;
                aud.volume = 100;
                aud.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
     

    }

    private void LateUpdate()
    {
        Velocity = Mathf.Abs(Mathf.RoundToInt(rb.velocity.x));
        VelocityText.text = "Speed: " + Velocity;
        DistanceTravelled = Mathf.RoundToInt(Vector3.Distance(transform.position, StartPoint));
        DistanceText.text = "Distance Travelled: " + DistanceTravelled;

        if (DistanceTravelled > LongestDistance)
        {
            LongestDistance = Mathf.RoundToInt(DistanceTravelled);
            PlayerPrefs.SetInt("LongestDistance", Mathf.RoundToInt(DistanceTravelled));
            LongestDistanceText.text = "Longest Distance: " + DistanceTravelled;
            LongestDistanceText.color = Color.green;
            DistanceText.color = Color.green;
            
        }

        if (Velocity > HighestVelocity)
        {
            HighestVelocity = Mathf.RoundToInt(Velocity);
            PlayerPrefs.SetInt("HighestVelocity", Mathf.RoundToInt(Velocity));
            HighestVelocityText.text = "Highest Velocity: " + Velocity;
            HighestVelocityText.color = Color.green;
            VelocityText.color = Color.green;
        }
     
       
    }

    public void Boost()
    {
       
        Thrust(BoostForce);
        aud.clip = boostclip;
        aud.volume = 50;
        aud.Play();
        BoostParts.Play();
        BoostText.SetActive(false);
        HasBoost = false;
        
    }


    void Thrust(float ThrustMagnitude)  
    {
        rb.AddRelativeForce(Vector3.forward * ThrustMagnitude);
      
    }
    void UpThrust(float ThrustMagnitude)
    {
        rb.AddRelativeForce(Vector3.up * ThrustMagnitude);

    }

    void Strafe(float StrafeMagnitude)
    {
        rb.AddRelativeForce(Vector3.right * StrafeMagnitude);

    }
    void Torque(float TorqueMagnitude)
    {
        rb.AddRelativeTorque(Vector3.forward * TorqueMagnitude);
    }

    public void PlayPositive()
    {
        aud.clip = Positive;
        aud.volume = 100;
        aud.Play();
    }
    void Malfunction()
    {
        if (Fire)
        {
            Fire.Play();
        }
       
        //pick one randomlyVVVVV
        int randomnum = Random.Range(0, 2);
        MalfunctionText.SetActive(true);
      
        switch (randomnum)
        {
           
            case 0:
                //spin uncontrollably
                aud.clip = Malfunctionclip1;
                aud.Play();
                rb.AddRelativeTorque(Vector3.forward*(Random.Range(500,1000)));
                Invoke("PlayAlarm", Malfunctionclip1.length);
                break;
            case 1:
                rb.AddRelativeTorque(Vector3.forward * -(Random.Range(500, 1000)));
                aud.clip = Malfunctionclip;
                aud.Play();
                Invoke("PlayAlarm", Malfunctionclip.length);
                break;
            

              
        }


      

        


    }
    void PlayAlarm()
    {
        aud.clip = AlarmClip;
        aud.Play();
    }
}
