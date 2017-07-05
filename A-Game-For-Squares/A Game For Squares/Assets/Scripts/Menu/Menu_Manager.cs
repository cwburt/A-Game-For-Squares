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
    private int OptionsIndex;
    private int[] VolValues = { 5, 5 };
    [SerializeField]
    Sprite[] Numbers;
    [SerializeField]
    GameObject[] OptionSelections;
    //**CONTROLS**//
    //**QUIT**//
    private int QuitIndex;
    [SerializeField]
    GameObject[] QuitSelections;
    //**MISC**//
    //private bool isCameraPeak;
    //private bool isAtTheBottom;
    //private bool isLerpingDone;

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

    public int prop_QuitIndex
    {
        get
        {
            return QuitIndex;
        }

        set
        {
            QuitIndex = value;
        }
    }
    #endregion



    void Awake () {

        CurrentMenu = MENU.Title;
        //main//
        MainIndex = 0;
        firstGo = true;

        //controls//

        //options//
        OptionsIndex = 0;
        Expand(OptionSelections[OptionsIndex],0.001f);

        for(int x = 0; x < OptionSelections.Length - 1;++x)
            OptionSelections[x].GetComponent<SpriteRenderer>().sprite = Numbers[VolValues[x]];


        //quit//
        QuitIndex = 0;
        Expand(QuitSelections[prop_QuitIndex],0.002f);


        //misc//

    }
    void Update ()
    {
	    switch(CurrentMenu)
        {
            case MENU.Title:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(0, 0, TheCamera.transform.position.z), 2.0f * Time.deltaTime);
                    TheCamera.transform.rotation = Quaternion.Lerp(TheCamera.transform.rotation, new Quaternion(0, 0, 0, 0), 2.0f * Time.deltaTime);
                    if (Input.GetKeyDown(KeyCode.Return) && isEnterPressed == false)
                    {
                        T_PressEnter.GetComponent<Title_PressEnter_Flasher>().DoCoolThing();
                    }
                }
                break;
            case MENU.Main:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(0, -8, TheCamera.transform.position.z), 2.0f * Time.deltaTime);
                    MainMenuSelection();
                }
                break;
            case MENU.Options:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(8, 0, TheCamera.transform.position.z), 2.0f * Time.deltaTime);
                    OptionSelection();
                }
                break;
            case MENU.Controls:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(-8, 0, TheCamera.transform.position.z), 2.0f * Time.deltaTime);
                }
                break;
            case MENU.Quit:
                {
                    TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position,new Vector3(0, 8, TheCamera.transform.position.z), 2.0f * Time.deltaTime);
                    QuitSelection();   
                }
                break;
        }
	}
    //**MAIN MENU FUNCTIONS**//
    void MainMenuSelection()
    {
        if(firstGo == true)
        {
            Expand(MainSelections[prop_MainIndex], 0.002f);
            Main_Selection_Reposition(MainSelections[prop_MainIndex], true);
            MainSelections[prop_MainIndex].transform.GetChild(0).gameObject.SetActive(true);// Turn on A E S T H E T I C Spawners
            firstGo = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && prop_MainIndex < MainSelections.Length - 1)
        {
            Shrink(MainSelections[prop_MainIndex],0.002f);
            Main_Selection_Reposition(MainSelections[prop_MainIndex], false);
            MainSelections[prop_MainIndex].transform.GetChild(0).gameObject.SetActive(false);

            prop_MainIndex += 1;
            if (prop_MainIndex == MainSelections.Length)
                prop_MainIndex = 0;

            Expand(MainSelections[prop_MainIndex], 0.002f);
            Main_Selection_Reposition(MainSelections[prop_MainIndex], true);

            MainSelections[prop_MainIndex].transform.GetChild(0).gameObject.SetActive(true);// Turn on A E S T H E T I C Spawners
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && prop_MainIndex > 0)
        {
            Shrink(MainSelections[prop_MainIndex],0.002f);
            Main_Selection_Reposition(MainSelections[prop_MainIndex], false);
            MainSelections[prop_MainIndex].transform.GetChild(0).gameObject.SetActive(false);

            prop_MainIndex -= 1;
            if (prop_MainIndex == -1)
                prop_MainIndex = MainSelections.Length - 1;

            Expand(MainSelections[prop_MainIndex],0.002f);
            Main_Selection_Reposition(MainSelections[prop_MainIndex], true);

            MainSelections[prop_MainIndex].transform.GetChild(0).gameObject.SetActive(true);// Turn on A E S T H E T I C Spawners
        }


        //On Enter Press
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (prop_MainIndex)
            {
                case 0:
                    SceneManager.LoadScene(1);
                    break;
                case 1:
                    CurrentMenu = MENU.Options;
                    //isLerpingDone = false;
                    //isAtTheBottom = false;
                    break;
                case 2:
                    CurrentMenu = MENU.Controls;
                    //isLerpingDone = false;
                    //isAtTheBottom = false;
                    break;
                case 3:
                    CurrentMenu = MENU.Quit;
                    //isLerpingDone = false;
                    //isAtTheBottom = false;
                    break;
            }
        }
    }
    void Main_Selection_Reposition(GameObject _object, bool _selected)
    {
        if (_selected)
        {
            for (int x = 0; x < 100; ++x)
            {
                _object.transform.position = new Vector3((_object.transform.position.x + 0.008f), _object.transform.position.y, _object.transform.position.z);
            }
        }
        else
        {
            for (int x = 0; x < 100; ++x)
            {
                _object.transform.position = new Vector3((_object.transform.position.x - 0.008f), _object.transform.position.y, _object.transform.position.z);
            }

        }
    }
    //**OPTION MENU FUNCTIONS**//
    void OptionSelection()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Shrink(OptionSelections[OptionsIndex],0.001f);
            OptionsIndex -= 1;
            if (OptionsIndex == -1)
                OptionsIndex = OptionSelections.Length - 1;
            Expand(OptionSelections[OptionsIndex],0.001f);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Shrink(OptionSelections[OptionsIndex],0.001f);
            OptionsIndex += 1;
            if (OptionsIndex == OptionSelections.Length)
                OptionsIndex = 0;
            Expand(OptionSelections[OptionsIndex], 0.001f);

        }
        else if (OptionsIndex != OptionSelections.Length - 1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && VolValues[OptionsIndex] != 9)
                VolValues[OptionsIndex]++;
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && VolValues[OptionsIndex] != 0)
                VolValues[OptionsIndex]--;

            OptionSelections[OptionsIndex].GetComponent<SpriteRenderer>().sprite = Numbers[VolValues[OptionsIndex]];
        }
        else if(OptionsIndex == OptionSelections.Length - 1 && Input.GetKeyDown(KeyCode.Return))
        {
            Shrink(OptionSelections[OptionsIndex],0.001f);
            OptionsIndex = 0;
            Expand(OptionSelections[OptionsIndex], 0.001f);
            CurrentMenu = MENU.Main;
        }
    }
    //**QUIT MENU FUNCTIONS**//
    void QuitSelection()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Shrink(QuitSelections[prop_QuitIndex],0.002f);
            prop_QuitIndex += 1;
            if (prop_QuitIndex == QuitSelections.Length)
                prop_QuitIndex = 0;
            Expand(QuitSelections[prop_QuitIndex],0.002f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Shrink(QuitSelections[prop_QuitIndex],0.002f);
            prop_QuitIndex -= 1;
            if (prop_QuitIndex == -1)
                prop_QuitIndex = QuitSelections.Length - 1;
            Expand(QuitSelections[prop_QuitIndex],0.002f);
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch(prop_QuitIndex)
            {
                case 0:
                    Application.Quit();
                    break;
                case 1:
                    CurrentMenu = MENU.Main;
                    break;
            }
        }
    }






    //**MISC FUNCTIONS**//
    void Shrink(GameObject _object, float _scaleRate)
    {
        for (int x = 0; x < 100; ++x)
        {
            //_object.transform.position = new Vector3((_object.transform.position.x - 0.008f), _object.transform.position.y, _object.transform.position.z);
            _object.transform.localScale = new Vector3(_object.transform.localScale.x - _scaleRate, _object.transform.localScale.y - _scaleRate, _object.transform.localScale.z);
        }
    }
    void Expand(GameObject _object, float _scaleRate)
    {
        for (int x = 0; x < 100; ++x)
        {
            //_object.transform.position = new Vector3((_object.transform.position.x - 0.008f), _object.transform.position.y, _object.transform.position.z);
            _object.transform.localScale = new Vector3(_object.transform.localScale.x + _scaleRate, _object.transform.localScale.y + _scaleRate, _object.transform.localScale.z);
        }
    }
    /*void CameraLerpZ(float To)
{
   //Debug.Log(TheCamera.transform.position.z);
   //Debug.Log(To - 2);
   if (TheCamera.transform.position.z < (To + 2) && !isAtTheBottom)
       isCameraPeak = true;


   if (!isCameraPeak && !isAtTheBottom)
       TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(TheCamera.transform.position.x, TheCamera.transform.position.y, To), 2.0f * Time.deltaTime);
   else if (isCameraPeak && !isAtTheBottom)
       TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(TheCamera.transform.position.x, TheCamera.transform.position.y, -10), 2.0f * Time.deltaTime);


   if (TheCamera.transform.position.z > -10.003 && isCameraPeak == true)
   {
       TheCamera.transform.position = Vector3.Lerp(TheCamera.transform.position, new Vector3(TheCamera.transform.position.x, TheCamera.transform.position.y, -10), 1);
       isCameraPeak = false;
       isAtTheBottom = true;
       isLerpingDone = true;
   }

   //Debug.Log("C " + isCameraPeak);
   //Debug.Log("B " + isAtTheBottom);
   //Debug.Log("L" + isLerpingDone);
}*/

    IEnumerator Wait()
    {
        Debug.Log("wait");
        yield return new WaitForSeconds(0.01f);
    }

}
