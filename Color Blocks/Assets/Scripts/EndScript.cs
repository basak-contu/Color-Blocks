using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] Button continueButton;

    Score score;

    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<Score>();
        if (score.getDiamondCount() - score.getNeededDiamondCount() >= 0)
        {
            continueButton.gameObject.SetActive(true);
        }
        ShowFinalScore();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Score " + score.getScore().ToString();
    }
    
    public void spendDiamond()
    {
        int diamond_count = score.getDiamondCount();

        if(diamond_count - score.getNeededDiamondCount() > 0)
        {
            score.setDiamondCount(diamond_count-2);
            FindObjectOfType<GameManager>().Resume(); 
        }
        else
        {
            FindObjectOfType<GameManager>().PlayAgain(); 
        }
    }
}
