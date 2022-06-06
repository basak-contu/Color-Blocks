using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public static bool gameIsPaused = false;

    public GameObject gameOverCanvas;
    [SerializeField] Button continueButton;

    public PlayerControl playerControl;



    public void EndGame(){
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            gameOverCanvas.SetActive(true);
            TinySauce.OnGameFinished((float) playerControl.score);
            if (playerControl.diamond_count >= playerControl.neededDiamondCount)
            {
                continueButton.gameObject.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        gameOverCanvas.SetActive(false);

        playerControl.ResumePlayer();
        
        gameIsPaused = false;
       
    }

    public void Pause()
    {
        gameOverCanvas.SetActive(true);
        if (playerControl.diamond_count >= playerControl.neededDiamondCount)
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void PlayAgain()
    {
        if(gameIsPaused == true)
        {
            Resume();
        }
        EndGame();
        SceneManager.LoadScene(1);
    }
}
