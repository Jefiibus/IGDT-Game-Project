using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager: MonoBehaviour
{
    private static float score;
    public static float timeFromStart;
    public static int totalScore;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0f; timeFromStart = 0f;
        UpdateScore(score);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        timeFromStart += Time.deltaTime;
        totalScore = (int)(score + timeFromStart);
        scoreText.text = "Score: " + totalScore;
    }

    public void UpdateScore(float scoreToAdd)
    {
        score += scoreToAdd;
        totalScore = (int)(score + timeFromStart);
        scoreText.text = "Score: " + totalScore;
    }

}
