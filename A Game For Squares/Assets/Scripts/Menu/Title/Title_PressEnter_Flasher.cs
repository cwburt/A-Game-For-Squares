using UnityEngine;
using System.Collections;

public class Title_PressEnter_Flasher : MonoBehaviour {

    private bool isLightingUp = true;
    private bool EnterPressed = false;
    private bool isDoneBeingPressed = false;

    public GameObject CoolThing_Shadow;

    private SpriteRenderer opac; // Opacity Variable
    private float fade;//Alpha Number
                       // Use this for initialization
    void Awake()
    {
        opac = GetComponent<SpriteRenderer>();//This is the sprite and its color pallet
        fade = 0f;
    }
	// Update is called once per frame
	void Update () {
        if (EnterPressed == false)
        {
            if (fade >= 1f)
                isLightingUp = false;
            if (fade <= 0f)
                isLightingUp = true;

            if (isLightingUp == true)
                fade += .02f;
            else
                fade -= .02f;


            Color col = opac.color;
            col.a = fade;
            opac.color = col;
        }
        else
        {
            fade = 1f;
            Color col = opac.color;
            col.a = fade;
            opac.color = col;
        }
	}
    public void DoCoolThing()
    {
        EnterPressed = true;
        Instantiate(CoolThing_Shadow, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
