using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int musicPlayerCount = FindObjectsOfType(GetType()).Length;
        if (musicPlayerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
