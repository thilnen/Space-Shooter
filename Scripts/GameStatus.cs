using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameStatus : MonoBehaviour
{
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        SetUpSingleton();
    }


    private void SetUpSingleton()
    {
        int scoreCount = FindObjectsOfType<GameStatus>().Length;
        if (scoreCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
    public void AddToScore(int scoreValue)
    {
        currentScore += scoreValue;
    }

    public void GameReset()
    {
        Destroy(gameObject);
    }
}
