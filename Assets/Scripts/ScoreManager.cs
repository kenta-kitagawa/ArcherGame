using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance = null;

    private static int score = 10;

    private static Text scoreText;

    public static ScoreManager GetInstance()
    {
        if (instance != null) return instance;

        GameObject singletonObj = new GameObject("ScoreManager");
        DontDestroyOnLoad(singletonObj);
        instance = (ScoreManager)singletonObj.AddComponent(typeof(ScoreManager));
        return instance;
    }

    public void Initialize(Text _scoreText)
    {
        scoreText = _scoreText;
        scoreText.text = score.ToString();
    }


    public int AddScore(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();
        return score;
    }
}
