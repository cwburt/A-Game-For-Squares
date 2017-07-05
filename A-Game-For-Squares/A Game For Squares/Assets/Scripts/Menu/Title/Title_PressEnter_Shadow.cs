using UnityEngine;
using System.Collections;

public class Title_PressEnter_Shadow : MonoBehaviour {

    private SpriteRenderer opac; // Opacity Variable
    private float fade;//Alpha Number

    void Awake()
    {
        FindObjectOfType<Menu_Manager>().prop_IsEnterPressed = true;
        opac = GetComponent<SpriteRenderer>();//This is the sprite and its color pallet
        fade = 1f;
    }	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector2(transform.localScale.x + 0.008f, transform.localScale.y + 0.008f);

        fade -= .03f;
        Color col = opac.color;
        col.a = fade;
        opac.color = col;
        if(fade <= 0f)
        {
            FindObjectOfType<Menu_Manager>().prop_IsEnterPressed = false;
            FindObjectOfType<Menu_Manager>().CurrentMenu = Menu_Manager.MENU.Main;
            Destroy(gameObject);
        }
    }
}
