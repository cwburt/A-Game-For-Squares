using UnityEngine;
using System.Collections;

public class Menu_Manager : MonoBehaviour {
    public Camera TheCamera; //The Main Camera for the Menu Scene


    public enum MENU //Enum that contains all the menus except the Credits Screen (which isn't really a menu)
    {
        Title,
        Main,
        Options,
        Controls,
        Quit
    };
    
    public MENU CurrentMenu;// Self Explanitory

    //**TITLE**//
    bool isEnterPressed = false;
    public GameObject T_PressEnter;

    //**MAIN**//
    //**OPTIONS**//
    //**CONTROLS**//
    //**QUIT**//
    public bool prop_IsEnterPressed
    {
        get
        {
            return isEnterPressed;
        }

        set
        {
            isEnterPressed = value;
        }
    }



    // Use this for initialization
    void Start () {
        CurrentMenu = MENU.Title;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    switch(CurrentMenu)
        {
            case MENU.Title:
                {
                    
                    TheCamera.transform.position = new Vector3(0, 0, -10);
                    TheCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
                    if(Input.GetKeyDown(KeyCode.Return) && isEnterPressed == false)
                    {
                        T_PressEnter.GetComponent<Title_PressEnter_Flasher>().DoCoolThing();
                        
                    }
                }
                break;
            case MENU.Main:
                {
                    TheCamera.transform.position = new Vector3(0, -8, -10);
                    TheCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                break;
            case MENU.Options:
                {
                    TheCamera.transform.position = new Vector3(8, 0, -10);
                    TheCamera.transform.localEulerAngles = new Vector3(0, 0, 90);
                }
                break;
            case MENU.Controls:
                {
                    TheCamera.transform.position = new Vector3(-8, 0, -10);
                    TheCamera.transform.localEulerAngles = new Vector3(0, 0, 270);
                }
                break;
            case MENU.Quit:
                {
                    TheCamera.transform.position = new Vector3(0, 0, -10);
                    TheCamera.transform.localEulerAngles = new Vector3(0, 0, 180);
                }
                break;

        }
	}
 
}
