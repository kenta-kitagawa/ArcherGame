using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance = null;

    private static int score = 0;

    public static ScoreManager GetInstance()
    {
        if (instance != null) return instance;

        GameObject singletonObj = new GameObject("ScoreManager");
        DontDestroyOnLoad(singletonObj);
        instance = (ScoreManager)singletonObj.AddComponent(typeof(ScoreManager));
        return instance;
    }


    public static int AddScore(int _score)
    {
        score += _score;
        return score;
    }
}
