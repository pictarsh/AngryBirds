using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentIndex = 0;
    public GameObject[] bee;
    public int enemyCount;

    public GameObject winFrame, loseFrame;
    
    private void Awake()
    {
        enemyCount = FindObjectsByType<EnemyScript>(FindObjectsSortMode.None).Length;
    }

    public void activateNextBee()
    {
        currentIndex++;
        if (currentIndex <= 2 )
        {
            print(currentIndex);
            bee[currentIndex].SetActive(true);
        }
        

        if (currentIndex > 2 && enemyCount > 0)
        {
            loseFrame.SetActive(true);
        }
        
        if (enemyCount == 0)
        {
            winFrame.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
