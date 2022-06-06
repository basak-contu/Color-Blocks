using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class Score : MonoBehaviour
{
    public GameObject player;
    public TMP_Text scoreText;
    public TMP_Text diamondText;

    PlayerControl playerControl;

    public int neededDiamondCount;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        neededDiamondCount = playerControl.neededDiamondCount;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = playerControl.score.ToString();
        diamondText.text = playerControl.diamond_count.ToString();
    }

    public int getScore()
    {
        playerControl = player.GetComponent<PlayerControl>();
        return playerControl.score;
    }

    public int getDiamondCount()
    {
        playerControl = player.GetComponent<PlayerControl> ();
        return playerControl.diamond_count;
    }

    public int getNeededDiamondCount()
    {
        playerControl = player.GetComponent<PlayerControl>();
        return playerControl.neededDiamondCount;
    }

    public void setDiamondCount(int diamond_count)
    {
        playerControl.diamond_count = diamond_count;
    }
}
