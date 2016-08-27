using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour {
    [SerializeField]
    Camera TheCamera; //The Main Camera for the Menu Scene

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
    [SerializeField]
    GameObject T_PressEnter;

    //**MAIN**//
    private int MainIndex; //Which Item is currently selected
    private bool firstGo;
    [SerializeField]
    GameObject[] MainSelections;// An Array of Items in the Array

    //**OPTIONS**//
    //**CONTROLS**//
    //**QUIT**//


    #region //**PROPERTIES**//
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
    public int prop_MainIndex
    {
        get
        {
            return MainIndex;
        }

        set
        {
            MainIndex = value;
        }
    }
    #endregion



    // Use this for initialization
    void Start () {
        CurrentMenu = MENU.Title;
        MainIndex = 0;
        firstGo = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    switch(CurrentMenu)
        {
            case MENU.Title:
                {
                    
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(0, 0, -10), 5.0f * Time.deltaTime);
                    TheCamera.transform.rotation = Quaternion.Lerp(TheCamera.transform.rotation, new Quaternion(0, 0, 0, 0), 2.0f * Time.deltaTime);
                    if (Input.GetKeyDown(KeyCode.Return) && isEnterPressed == false)
                    {
                        T_PressEnter.GetComponent<Title_PressEnter_Flasher>().DoCoolThing();
                    }
                }
                break;
            case MENU.Main:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(0, -8, -10), 2.0f * Time.deltaTime);
                    TheCamera.transform.eulerAngles = Vector3.Lerp(TheCamera.transform.eulerAngles, new Vector3(0, 0, 0), 2.0f * Time.deltaTime);
                    MainMenuSelection();
                }
                break;
            case MENU.Options:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(8, 0, -10), 2.0f * Time.deltaTime);
                    TheCamera.transform.eulerAngles = Vector3.Lerp(TheCamera.transform.eulerAngles, new Vector3(0, 0, 90), 2.0f * Time.deltaTime);
                }
                break;
            case MENU.Controls:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(-8, 0, -10), 2.0f * Time.deltaTime);
                    TheCamera.transform.eulerAngles = Vector3.Lerp(TheCamera.transform.eulerAngles, new Vector3(0, 0, 270), 2.0f * Time.deltaTime);
                }
                break;
            case MENU.Quit:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position,new Vector3(0, 8, -10), 2.0f * Time.deltaTime);
                    TheCamera.transform.eulerAngles = Vector3.Lerp(TheCamera.transform.eulerAngles, new Vector3(0, 0, 180),2.0f * Time.deltaTime);
                }
                break;
        }
	}
    //**MAIN MENU FUNCTIONS**//
    void MainMenuSelection()
    {
        if(firstGo == true)
        {
            Expand(MainSelections[prop_MainIndex]);
            MainSelections[prop_MainIndex].transform.GetChild(0).gameObject.SetActive(true);// Turn on A E S T H E T I C Spawners
            firstGo = false;
        }
        



        if (Input.GetKeyDown(KeyCode.DownArrow) && prop_MainIndex < MainSelections.Length - 1)
        {
            prop_MainIndex += 1;
            Expand(MainSelections[prop_MainIndex]);
            MainSelections[prop_MainIndex].transform.GetChild(0).gameObject.SetActive(true);// Turn on A E S T H E T I C Spawners
            if (prop_MainIndex > 0)
            {
                MainSelections[prop_MainIndex - 1].transform.GetChild(0).gameObject.SetActive(false);
                Shrink(MainSelections[prop_MainIndex - 1]);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && prop_MainIndex > 0)
        {
            prop_MainIndex -= 1;
            Expand(MainSelections[prop_MainIndex]);
            MainSelections[prop_MainIndex].transform.GetChild(0).gameObject.SetActive(true);// Turn on A E S T H E T I C Spawners
            if (prop_MainIndex < MainSelections.Length)
            {
                MainSelections[prop_MainIndex + 1].transform.GetChild(0).gameObject.SetActive(false);
                Shrink(MainSelections[prop_MainIndex + 1]);
            }
        }


        //On Enter Press 
        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch(prop_MainIndex)
            {
                case 0:
                    SceneManager.LoadScene(1);
                    break;
                case 1:
                    CurrentMenu = MENU.Options;
                    break;
                case 2:
                    CurrentMenu = MENU.Controls;
                    break;
                case 3:
                    CurrentMenu = MENU.Quit;
                    break;
            }
        }
    }
    void Expand(GameObject _object) //Game Object git big and moves it to the right a little (Only for the Main Menu)
    {
        for(int x = 0;x < 10; ++x)
        {
            //Debug.Log(_object.transform.localScale.x);
            _object.transform.position = new Vector3((_object.transform.position.x + 0.08f), _object.transform.position.y, _object.transform.position.z);
            _object.transform.localScale = new Vector3(_object.transform.localScale.x + 0.02f, _object.transform.localScale.y + 0.02f, _object.transform.localScale.z);
        }
    }
    void Shrink(GameObject _object)//Game Object goes back to its original size and goes back to to its original position (Only for the Main Menu)
    {
        for (int x = 0; x < 10; ++x)
        {
            _object.transform.position = new Vector3((_object.transform.position.x - 0.08f), _object.transform.position.y, _object.transform.position.z);
            _object.transform.localScale = new Vector3(_object.transform.localScale.x - 0.02f, _object.transform.localScale.y - 0.02f, _object.transform.localScale.z);
        }
    }
 
}
