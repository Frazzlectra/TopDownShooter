using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    //hight score maybe
    public static int highScore = 0;
    //buttons presses
    public Button storyMode;
    public Button endless;
    public Button quitBtn;
    //select a mode
    public Button softBtn;
    public Button medBtn;
    public Button hardBtn;
    //select mode button animation
    public Animator animSoft;
    public Animator animMed;
    public Animator animHard;
    public static int core;

    void Start()
    {
        ////setUp Buttons
        //storyMode.enabled = true;
        //endless.enabled = true;
        //quitBtn.enabled = true;
        //ON Click
        
        core = 0;
        storyMode.onClick.AddListener(() => { ButtonOnClick("storyMode"); });
        endless.onClick.AddListener(() => { ButtonOnClick("endless"); });
        quitBtn.onClick.AddListener(() => { ButtonOnClick("quit"); });
        //select mode
        softBtn.onClick.AddListener(() => { ButtonOnClick("softBtn"); });
        medBtn.onClick.AddListener(() => { ButtonOnClick("medBtn"); });
        hardBtn.onClick.AddListener(() => { ButtonOnClick("hardBtn"); });
    }

    private void ButtonOnClick(string btn)
    {
        //Debug.Log("Button Clicked");
        if (btn == "storyMode" && core != 0)//story mode button enter story mode
        {
            EnemyManager.endless = false;
           // Debug.Log("StoryMode");
            Application.LoadLevel(1);
        }
        else if (btn == "endless" && core != 0)//endless mode 
        {
            EnemyManager.endless = true;//so enemys spawn endlessly 
            //Debug.Log("Endless");
            Application.LoadLevel(1);
        }
        else if (btn == "quit")
        {
            Application.Quit();
        }
        //select mode
        if (btn == "softBtn")
        {
            if (!animSoft.GetBool("softcoreOn"))
            {
                core = 1;
                animSoft.SetBool(("softcoreOn"), true);
                animMed.SetBool(("medcoreOn"), false);
                animHard.SetBool(("hardcoreOn"), false);
                Debug.Log("softcore");
            }
            else
            {
                animSoft.SetBool(("softcoreOn"), false);
                core = 0;
            }

        }
        else if (btn == "medBtn")
        {
            Debug.Log("medcore");
            if (!animMed.GetBool("medcoreOn"))
            {
                core = 2;
                animSoft.SetBool(("softcoreOn"), false);
                animMed.SetBool(("medcoreOn"), true);
                animHard.SetBool(("hardcoreOn"), false);
            }
            else
            {
                animMed.SetBool(("medcoreOn"), false);
                core = 0;
            }

        }
        else if (btn == "hardBtn")
        {
            if (!animHard.GetBool("hardcoreOn"))
            {
                core = 3;
                animSoft.SetBool(("softcoreOn"), false);
                animMed.SetBool(("medcoreOn"), false);
                animHard.SetBool(("hardcoreOn"), true);
                Debug.Log("hardcore");
            }
            else
            {
                animHard.SetBool(("hardcoreOn"), false);
                core = 0;
            }                    

        }
    }

}
