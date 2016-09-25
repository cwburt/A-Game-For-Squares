using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject bullet, Gun;
    
    [SerializeField]
    ParticleSystem LeftThrust, RightThrust;

    [SerializeField]
    Vector3 StartPos;

    [SerializeField]
    float speed, FireRate, internalCounter;

    [SerializeField]
    bool canFire;

    public float HP;
    public bool isAlive;

    void Start()
    {
        isAlive = true;

        if (speed <= 0)
            speed = 4.8f;

        if (FireRate <= 0)
            FireRate = .2f;

        internalCounter = FireRate;
    }

    void Update()
    {
        if (HP <= 0)
            isAlive = false;
    }

    void FixedUpdate ()
    {
        if (internalCounter < FireRate)
        {
            internalCounter += Time.deltaTime;
        }
        else if (!canFire)
            canFire = true;
            
        

        //Movement on directional. Also handles thruster particle start and stops
        if (Input.GetKey("right"))
        {
            if (LeftThrust.isPlaying)
                LeftThrust.Stop();

            transform.RotateAround(Vector3.zero, Vector3.forward, speed);

            RightThrust.Play();
        }
        else if (Input.GetKey("left"))
        {
            if (RightThrust.isPlaying)
                RightThrust.Stop();

            transform.RotateAround(Vector3.zero, Vector3.back, speed);

            LeftThrust.Play();
        }
        else
        {
            if (LeftThrust.isPlaying)
                LeftThrust.Stop();

            if (RightThrust.isPlaying)
                RightThrust.Stop();
        }

        if (Input.GetKey("space") && canFire)
        {
            StartPos = new Vector2(Gun.transform.position.x, Gun.transform.position.y);
            Instantiate(bullet, StartPos, Gun.transform.rotation);
            internalCounter = 0;
            canFire = false;
        }
    }
}
