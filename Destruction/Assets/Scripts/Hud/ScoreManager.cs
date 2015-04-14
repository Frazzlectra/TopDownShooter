using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    Text scoreText;
    public static int score;

    void Awake()
    {
        scoreText = GetComponent<Text>();
        score = 0;
    }
    void Update()
    {
        scoreText.text = "Score: " + score;
    }

}
