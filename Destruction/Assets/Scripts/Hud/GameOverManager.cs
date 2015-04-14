using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    Animator anim;
    public PlayerHealthScript playerHealth;
    public GameObject player;

    float freezeTimer;
    public Button restart;
    public Button quitToMain;
    public Text restartText;
    AudioSource playerAudio;
    //high score
    public Text highScoreText;
    //Items Changed by Hud and MainMenu
    public static bool openMenu;
    public static bool gameWon;
    bool isOver = false;
    void Awake()
    {
        highScoreText.text = "High Score:" + MainMenuScript.highScore;
        playerAudio = player.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        ////buttons
        //restartText = restart.GetComponentInChildren<Text>();
        //Debug.Log("restartText" + restartText.text);
        restart.onClick.AddListener(() => { ButtonOnClick("restart"); });
        quitToMain.onClick.AddListener(() => { ButtonOnClick("toMain"); });        
    }

    void ButtonOnClick(string btn)
    {
        Debug.Log("ButtonClicked");
        if (btn == "restart")
        {

            //Debug.Log("restart");
            isOver = false;//set everything back to begining
            //gameWon = false;            
            EnemyManager.theBoss = false;
            EnemyManager.numEnemies = 0;
            EnemyManager.bossFight = false;
            playerAudio.enabled = true;
            //turn off animation
            anim.SetBool(("GameOver"), false);
            anim.SetBool(("GameWon"), false);
            if (gameWon)
            {
                if (Application.loadedLevel < 2)
                {
                    Application.LoadLevel(Application.loadedLevel + 1);
                    gameWon = false;
                }
                else
                {
                    Application.LoadLevel(0);
                }
            }
            else
            {
                    Application.LoadLevel(Application.loadedLevel);
            }
        }
        else if (btn == "toMain")
        {
            isOver = false;//set everything back to begining
            gameWon = false;
            EnemyManager.theBoss = false;
            EnemyManager.numEnemies = 0;
            EnemyManager.bossFight = false;
            playerAudio.enabled = true;
            //turn off animation
            anim.SetBool(("GameOver"), false);
            anim.SetBool(("GameWon"), false);
            //high score to zero;
            MainMenuScript.highScore = 0;
            //back to main menu
            Application.LoadLevel(0);
        }
    }
    void Update()
    {
        if (gameWon)
        {
            //won
            if (MainMenuScript.highScore < ScoreManager.score)
            {
                MainMenuScript.highScore = ScoreManager.score;
                highScoreText.text = "High Score: " + MainMenuScript.highScore;
            }
            anim.SetBool(("GameWon"), true);
            playerAudio.enabled = false;
            restartText.text = "Onwards";
        }
        else if (playerHealth.currentHealth <= 0f && !isOver)
        {
            if (MainMenuScript.highScore < ScoreManager.score)
            {
                MainMenuScript.highScore = ScoreManager.score;
                highScoreText.text = "High Score: " + MainMenuScript.highScore;
            }
            //lost 
            isOver = true;
            anim.SetBool(("GameOver"), true);
            playerAudio.enabled = false;
            restartText.text = "Restart";
        }
    }
}
