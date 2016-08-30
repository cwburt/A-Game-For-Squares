using UnityEngine;
using System.Collections;

public class Ship : Entity
{
    [SerializeField]
    GameObject bullet, Gun;

    [SerializeField]
    Vector3 StartPos;

    [SerializeField]
    float speed, FireRate, internalCounter;

    [SerializeField]
    bool canFire;

    protected override void Start()
    {
        base.Start();

        if (speed <= 0)
            speed = 2.4f;

        if (FireRate <= 0)
            FireRate = .2f;

        internalCounter = FireRate;
    }

    void FixedUpdate ()
    {
        if (internalCounter < FireRate)
        {
            internalCounter += Time.deltaTime;
        }
        else if (!canFire)
            canFire = true;
            
        

        //Movement on directional
        if (Input.GetKey("right"))
        {
            //transform.Rotate(0, 0, -(speed));
            transform.RotateAround(Vector3.zero, Vector3.forward, speed);
        }
        else if (Input.GetKey("left"))
        {
            //transform.Rotate(0, 0, speed);
            transform.RotateAround(Vector3.zero, Vector3.back, speed);
        }

        if(Input.GetKey("space") && canFire)
        {
            StartPos = new Vector2(Gun.transform.position.x, Gun.transform.position.y);
            Instantiate(bullet, StartPos, Gun.transform.rotation);
            internalCounter = 0;
            canFire = false;
        }
    }
}
