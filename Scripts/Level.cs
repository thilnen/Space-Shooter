using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2;
    public void LoadGameOver()
    {
        StartCoroutine(GameOverAfterDelay());
        
    }

    IEnumerator GameOverAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
       
    }

    public void LoadGameScene()
    {
        
        SceneManager.LoadScene(1);
        FindObjectOfType<GameStatus>().GameReset();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
