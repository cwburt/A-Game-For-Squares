﻿using UnityEngine;
using System.Collections;

public class Vortex : MonoBehaviour
{
    [SerializeField]
    GameObject Asteroid;

    [SerializeField]
    float RotateSpeed, FadeSpeed, ScaleSpeed, SpawnMax, SpawnMin, AstMinSize, AstMaxSize;

    [SerializeField]
    bool Spawn;

    Vector3 ScaleLoop;
    bool flip;
    float InternalClock;
    Color Opacity;


    void Start ()
    {
        ScaleLoop = transform.localScale;
        flip = true;
        Spawn = false;
        Opacity = GetComponent<SpriteRenderer>().color;

        if (FadeSpeed == 0)
            FadeSpeed = .005f;

        if (RotateSpeed == 0)
            RotateSpeed = 1.5f;

        if (ScaleSpeed == 0)
            ScaleSpeed = 0.0025f;

        if (AstMinSize == 0)
            AstMinSize = .5f;

        if (AstMaxSize == 0)
            AstMaxSize = 1f;

        if (SpawnMax == 0)
        {
            //Based on difficulty setting?
            SpawnMax = 1.5f;
            SpawnMin = .5f;
        }

        InternalClock = 0;
    }
	
	void FixedUpdate ()
    {
        if(gameObject.GetComponent<SpriteRenderer>().color.a < 1)
            FadeIn();
        Scale();
        Rotate();

        InternalClock += Time.deltaTime;
        if (InternalClock >= Random.Range(SpawnMin, SpawnMax))
       {
            GameObject temp;
            InternalClock = 0;
            temp = Instantiate(Asteroid, transform.position, Quaternion.identity) as GameObject;

            float randomSize = Random.Range(AstMinSize, AstMaxSize);
            Vector3 ScaleSize = new Vector3(randomSize, randomSize, 1);
            temp.transform.localScale = ScaleSize;

            float X = Random.Range(-1f, 1f) * Random.Range(75, 200);
            float Y = Random.Range(-1f, 1f) * Random.Range(75, 200);
            Vector3 Direction = new Vector3(X, Y, 0);

            temp.GetComponent<Rigidbody>().AddForce(Direction);
        }
	}

    void FadeIn()
    {
        if (GetComponent<SpriteRenderer>().color.a < 1)
        {
            GetComponent<SpriteRenderer>().color = Opacity;
            Opacity.a += FadeSpeed;
        }
    }

    void Scale()
    {
        if (transform.localScale.x >= .5f && transform.localScale.y >= .5f && flip)
            flip = false;
        else if (transform.localScale.x < .2f && transform.localScale.y < .2f && !flip)
            flip = true;

        if (flip)
        {
            ScaleLoop.x += ScaleSpeed;
            ScaleLoop.y += ScaleSpeed;

            transform.localScale = ScaleLoop;
        }
        else if (!flip)
        {
            ScaleLoop.x -= ScaleSpeed;
            ScaleLoop.y -= ScaleSpeed;

            transform.localScale = ScaleLoop;
        }
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, -RotateSpeed));
    }
}
