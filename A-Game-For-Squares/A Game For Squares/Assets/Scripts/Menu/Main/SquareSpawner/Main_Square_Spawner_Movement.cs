using UnityEngine;
using System.Collections;

public class Main_Square_Spawner_Movement : MonoBehaviour {

    //The Square
    public bool isRight;
    private int TimeCounter; //To Help Space out the Update

    private SpriteRenderer opac; // Opacity Variable
    private float fade;//Alpha Number

    void Start()
    {
        TimeCounter = 0;
    }
    void Awake()
    {
        //transform.localScale = new Vector2(0.15f, 0.15f);
        opac = GetComponent<SpriteRenderer>();//This is the sprite and its color pallet
        fade = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter++;
        if (fade != 1f)
        {
            fade += .03f;
            Color col = opac.color;
            col.a = fade;
            opac.color = col;
        }

        if (isRight)
        {
            if (TimeCounter % 2 == 1)
            {
                transform.Rotate(0, 0, -6.0f);//Rotates 5 Degrees Counter Clockwise
                transform.position = new Vector2((transform.position.x + 0.06f), transform.position.y);//Moves Right

            }
            if (TimeCounter == 5)
            {
               transform.localScale = new Vector2((transform.localScale.x - 0.005f), (transform.localScale.y - 0.005f));// Scales Down
                TimeCounter = 0;
            }
        }
        else
        {
            if (TimeCounter % 2 == 1)
            {
                transform.Rotate(0, 0, 5.0f);//Rotates 5 Degrees Clockwise
                transform.position = new Vector2((transform.position.x - 0.03f), transform.position.y);// Moves Left
            }
            if (TimeCounter == 5)
            {
                transform.localScale = new Vector2((transform.localScale.x - 0.02f), (transform.localScale.y - 0.02f));// Scales Down
                TimeCounter = 0;
            }
        }
        if (transform.localScale.x <= 0.00)
        {
            Destroy(gameObject);//kys
        }
    }
}
