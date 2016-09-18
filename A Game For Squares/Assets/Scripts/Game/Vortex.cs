using UnityEngine;
using System.Collections;

public class Vortex : MonoBehaviour
{
    [SerializeField]
    float RotateSpeed, FadeSpeed, ScaleSpeed;

    Color Opacity;
    Vector3 ScaleLoop;
    bool flip;

	void Start ()
    {
        Opacity = GetComponent<SpriteRenderer>().color;
        ScaleLoop = transform.localScale;
        flip = true;

        if (FadeSpeed == 0)
            FadeSpeed = .005f;

        if (RotateSpeed == 0)
            RotateSpeed = 1.5f;

        if (ScaleSpeed == 0)
            ScaleSpeed = 0.0025f;
	}
	
	void FixedUpdate ()
    {
        if(GetComponent<SpriteRenderer>().color.a < 1)
        {
            GetComponent<SpriteRenderer>().color = Opacity;
            Opacity.a += FadeSpeed;
        }

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
        else if(!flip)
        {
            ScaleLoop.x -= ScaleSpeed;
            ScaleLoop.y -= ScaleSpeed;

            transform.localScale = ScaleLoop;
        }

        transform.Rotate(new Vector3(0, 0, -RotateSpeed));
	}
}
