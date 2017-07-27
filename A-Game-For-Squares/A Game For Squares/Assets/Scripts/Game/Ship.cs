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
        canFire = true;

        if (speed <= 0)
            speed = 4.8f;

        if (FireRate <= 0)
            FireRate = .2f;

        if (Difficulty.ChosenSetting == (int)Setting.Hard)
            HP = 1;
        else
            HP = 2;

        internalCounter = FireRate;
    }

    void Update()
    {
        if (HP <= 0)
        {
            isAlive = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void FixedUpdate ()
    {
        if (internalCounter < FireRate)
            internalCounter += Time.deltaTime;
        else if (!canFire && isAlive)
            canFire = true;
            
        //Movement on directional. Also handles thruster particle start and stops
        if (Input.GetKey("right"))
        {
            if (LeftThrust.isPlaying)
                LeftThrust.Stop();

            transform.RotateAround(Vector3.zero, Vector3.forward, speed);

            if(isAlive)
                RightThrust.Play();
        }
        else if (Input.GetKey("left"))
        {
            if (RightThrust.isPlaying)
                RightThrust.Stop();

            transform.RotateAround(Vector3.zero, Vector3.back, speed);

            if(isAlive)
                LeftThrust.Play();
        }
        else
        {
            if (LeftThrust.isPlaying)
                LeftThrust.Stop();

            if (RightThrust.isPlaying)
                RightThrust.Stop();
        }

        if (Input.GetKey("space") && canFire && isAlive)
        {
            StartPos = new Vector2(Gun.transform.position.x, Gun.transform.position.y);
            Instantiate(bullet, StartPos, Gun.transform.rotation);
            internalCounter = 0;
            canFire = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            HP--;

            if (LeftThrust.isPlaying)
                LeftThrust.Stop();

            if (RightThrust.isPlaying)
                RightThrust.Stop();
        }

        if (Difficulty.ChosenSetting != (int)Setting.Easy && other.tag == "Bullet")
            HP--;
    }
}
